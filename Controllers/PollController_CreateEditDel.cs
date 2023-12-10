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
using Azure.Core;
using QMSL.Services;

namespace QMSL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController_CreateEditDel : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;

        public PollController_CreateEditDel(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _config = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreatePoll(PollDto poll, string DoctorEmail)
        {
            if(await _dataContext.GeneralPolls.AnyAsync(x => x.Name.Equals(poll.Name)))
            {
                return BadRequest("Poll with this name already exist");
            }
            if (await _dataContext.Doctors.AnyAsync(x => x.Email.Equals(DoctorEmail)))
            {
                return BadRequest("Doctor with this email does not exist");
            }

            foreach(GeneralQuestionDto question in poll.Questions)
            {
                if (AuthVerifier.NameVerification(question.Name))
                {
                    return BadRequest("Bad name");
                }
            }



            return Ok(poll);
        }
    }
}
