using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QMSL.Dtos;
using QMSL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QMSL.Services;
using Azure.Core;

namespace QMSL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;

        public AuthController(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _config = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(UserDto request, bool type)
        {
            if (await _dataContext.Patients.AnyAsync(x => x.Email.Equals(request.Email)))
            {
                return BadRequest("Patient with this email aldready exists");
            }

            if (await _dataContext.Doctors.AnyAsync(x => x.Email.Equals(request.Email)))
            {
                return BadRequest("Doctor with this email aldready exists");
            }

            if (!AuthVerifier.RegisterVerification(request.Email, request.Password, request.Age,
                request.Sex, request.PhoneNumber, request.Name, request.Surname, request.Fathername))
                return BadRequest("Some fields were wrongly typed");

            CreatePasswordHash(request.Password, out byte[] passwordHash);
            if (type)
            {
                var user = new Patient()
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Fathername = request.Fathername,
                    PhoneNumber = request.PhoneNumber,
                    Sex = request.Sex,
                    Age = request.Age,
                    Email = request.Email,
                    Password = passwordHash
                };

                _dataContext.Patients.Add(user);

                await _dataContext.SaveChangesAsync();
            }

            return Ok();
        }
        [HttpPost("LogIn")]
        public async Task<ActionResult<string>> Login(string Email, string Password)
        {
            CreatePasswordHash(Password, out byte[] passwordHash);
            var user = await _dataContext.Doctors.FirstAsync(x => x.Email.Equals(Email)
            && x.Password.SequenceEqual(VerifyPasswordHash(Password)));
            string userType = "";
            int countFind = 0;

            if (await _dataContext.Patients.AnyAsync(x => x.Email.Equals(Email)))
            {
                userType = "Patient";
                countFind++;
            }else if (await _dataContext.Doctors.AnyAsync(x => x.Email.Equals(Email)))
            {
                userType = "Doctor";
                countFind++;
            }

            if(countFind == 2)
            {
                return BadRequest("There are 2 users with this Email, contact support please");
            }
            if (user == null || userType.Equals(""))
            {
                return BadRequest("Either Email or Password is incorrect");
            }
            if (!AuthVerifier.LoginVerification(Email, Password))
                return BadRequest("Some fields were wrongly typed");

            string token = CreateToken(user, userType);

            return Ok(token);
        }
        private string CreateToken(User user, string Role)
        {
            List<Claim> claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
                (
                    claims: claim,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                hmac.Key = System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value);
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private byte[] VerifyPasswordHash(string password)
        {
            byte[] passwordHash;
            using (var hmac = new HMACSHA512())
            {
                hmac.Key = System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value);
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return passwordHash;
        }
    }
}
