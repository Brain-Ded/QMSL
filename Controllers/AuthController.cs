﻿using Microsoft.AspNetCore.Mvc;
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
using System.Text;

namespace QMSL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration? _config;
        private readonly DoctorFactory doctorFactory;
        private readonly PatientFactory patientFactory;

        public AuthController(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _config = configuration;
            doctorFactory = new DoctorFactory();
            patientFactory = new PatientFactory();
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
                var user = (Patient)patientFactory.CreateUser(request);
                user.Password = passwordHash;

                _dataContext.Patients.Add(user);

                await _dataContext.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                var user = (Doctor)doctorFactory.CreateUser(request);
                user.Password = passwordHash;

                _dataContext.Doctors.Add(user);

                await _dataContext.SaveChangesAsync();
                return Ok(user);
            }

            
        }
        [HttpPost("LogIn")]
        public async Task<ActionResult<string>> Login(Credentials credentials)
        {
            var Email = credentials.Email;
            var Password = credentials.Password;
            CreatePasswordHash(Password, out byte[] passwordHash);
            User user = await _dataContext.Doctors.FirstOrDefaultAsync(x => x.Email.Equals(Email)
            && x.Password.SequenceEqual(VerifyPasswordHash(Password)));
            if(user == null)
            {
                user = await _dataContext.Patients.FirstOrDefaultAsync(x => x.Email.Equals(Email)
            && x.Password.SequenceEqual(VerifyPasswordHash(Password)));
            }
              
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
            SymmetricSecurityKey key;
            List<Claim> claim = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("email", user.Email),
                new Claim("role", Role)
            };

            if(_config != null)
                key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            else
                key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("mylolsecretsexwithyourmama"));

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
                if (_config != null)
                    hmac.Key = System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value);
                else
                    hmac.Key = System.Text.Encoding.UTF8.GetBytes("mylolsecretsexwithyourmama");
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private byte[] VerifyPasswordHash(string password)
        {
            byte[] passwordHash;
            using (var hmac = new HMACSHA512())
            {
                if (_config != null)
                    hmac.Key = System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value);
                else
                    hmac.Key = System.Text.Encoding.UTF8.GetBytes("mylolsecretsexwithyourmama");
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return passwordHash;
        }
    }
}
