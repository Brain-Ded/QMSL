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

        [HttpPost("CreatePoll")]
        public async Task<ActionResult<string>> CreatePoll(PollDto poll, string DoctorEmail)
        {
            if(await _dataContext.GeneralPolls.AnyAsync(x => x.Name.Equals(poll.Name)))
            {
                return BadRequest("Poll with this name already exist");
            }
            if (!await _dataContext.Doctors.AnyAsync(x => x.Email.Equals(DoctorEmail)))
            {
                return BadRequest("Doctor with this email does not exist");
            }

            Doctor doctor = await _dataContext.Doctors.FirstAsync(x => x.Email.Equals(DoctorEmail));

            foreach(GeneralQuestionDto question in poll.Questions)
            {
                if (AuthVerifier.NameVerification(question.Name))
                {
                    return BadRequest("Bad name");
                }
            }

            GeneralPoll generalPoll = new GeneralPoll()
            {
                Name = poll.Name,
                //Doctor = doctor,
                DoctorId = doctor.Id
            };

            _dataContext.GeneralPolls.Add(generalPoll);
            await _dataContext.SaveChangesAsync();

            generalPoll = _dataContext.GeneralPolls.First(x => x.Name.Equals(generalPoll.Name));

            List<GeneralQuestion> generalQuestions = new List<GeneralQuestion>();

            foreach (GeneralQuestionDto question in poll.Questions)
            {
                generalQuestions.Add(new GeneralQuestion()
                {
                    Name = question.Name,
                    GeneralPollId = generalPoll.Id,
                });
            }

            generalPoll.Questions = generalQuestions;
            await _dataContext.SaveChangesAsync();

            generalQuestions = _dataContext.GeneralPolls.First(x => x.Name.Equals(generalPoll.Name)).Questions;

            for(int i=0; i<generalQuestions.Count; ++i)
            {
                generalQuestions[i].Answers = new List<Answer>();
                for (int j=0; j<poll.Questions[i].Answers.Count; ++j)
                {
                    
                    generalQuestions[i].Answers.Add(new Answer()
                    {
                        GeneralQuestionId = generalQuestions[i].Id,
                        Text = poll.Questions[i].Answers[j].Text,
                    });
                }
            }

            
            await _dataContext.SaveChangesAsync();
            generalPoll = _dataContext.GeneralPolls.First(x => x.Name.Equals(generalPoll.Name));

            return Ok(generalPoll);
        }
    }
}
