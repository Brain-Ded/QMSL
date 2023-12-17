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
        private readonly PollsService _pollsService;

        public PollController_CreateEditDel(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _config = configuration;
            _pollsService = new PollsService();
        }

        [HttpPost("CreatePoll")]
        public async Task<ActionResult<string>> CreatePoll(PollDto poll)
        {
            if(await _dataContext.GeneralPolls.AnyAsync(x => x.Name.Equals(poll.Name)))
            {
                return BadRequest("Poll with this name already exist");
            }
            if (!await _dataContext.Doctors.AnyAsync(x => x.Email.Equals(poll.DoctorEmail)))
            {
                return BadRequest("Doctor with this email does not exist");
            }

            Doctor doctor = await _dataContext.Doctors.FirstAsync(x => x.Email.Equals(poll.DoctorEmail));

            foreach(GeneralQuestionDto question in poll.Questions)
            {
                if (AuthVerifier.TextVerification(question.Name))
                {
                    return BadRequest("Bad question name");
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
                generalQuestions[i].GeneralAnswers = new List<GeneralAnswer>();
                for (int j=0; j<poll.Questions[i].Answers.Count; ++j)
                {
                    if (AuthVerifier.TextVerification(poll.Questions[i].Answers[j].Text))
                    {
                        return BadRequest("Bad answer text");
                    }
                    generalQuestions[i].GeneralAnswers.Add(new GeneralAnswer()
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

        [HttpPost("CreateEditPoll")]
        public async Task<ActionResult<string>> CreatePoll(EditablePollDto poll)
        {
            if (!await _dataContext.Patients.AnyAsync(x => x.Email.Equals(poll.patientEmail)))
            {
                return BadRequest("Doctor with this email does not exist");
            }

            Patient patient = await _dataContext.Patients.FirstAsync(x => x.Email.Equals(poll.patientEmail));

            if (patient == null)
            {
                return BadRequest("Patient with this email doesn't exist");
            }

            foreach(EditableQuestionDto question in poll.Questions)
            {
                if (AuthVerifier.TextVerification(question.Name))
                {
                    return BadRequest("Bad question name");
                }
            }

            EditablePoll editablePoll = new EditablePoll()
            {
                Name = poll.Name,
                PatientId = patient.Id,
                AssignedAt = poll.AssignedAt,
                PassedAt = poll.PassedAt,
                IsPassed = poll.IsPassed,
            };

            _dataContext.EditablePolls.Add(editablePoll);
            await _dataContext.SaveChangesAsync();

            editablePoll = await _dataContext.EditablePolls
                .FirstAsync(x => x.PatientId == patient.Id && x.Name.Equals(poll.Name));

            List<EditableQuestion> questions = new List<EditableQuestion>();
            List<Comment> comments = new List<Comment>();

            for(int i=0; i<poll.Questions.Count; i++)
            {
                questions.Add(new EditableQuestion()
                {
                    Name= poll.Questions[i].Name,
                    ChoosenAnswer = poll.Questions[i].ChoosenAnswer,
                    EditablePollId = editablePoll.Id,
                });
            }

            _dataContext.EditableQuestions.AddRange(questions);
            await _dataContext.SaveChangesAsync();

            for(int i=0; i< poll.Comments.Count; i++)
            {
                comments.Add(new Comment()
                {
                    DoctorId = poll.Comments[i].DoctorId,
                    CommentedAt = poll.Comments[i].CommentedAt,
                    EditablePollId=editablePoll.Id,
                    Text = poll.Comments[i].Text,
                    type = poll.Comments[i].type
                });
            }

            _dataContext.Comments.AddRange(comments);
            await _dataContext.SaveChangesAsync();

            List<EditableQuestion> editableQuestions = _dataContext.EditableQuestions
                .Where(x => x.EditablePollId == editablePoll.Id).ToList();

            for(int i=0; i<editableQuestions.Count; ++i)
            {
                for(int j=0; j < poll.Questions[i].EditableAnswers.Count; ++j)
                {
                    if (AuthVerifier.TextVerification(poll.Questions[i].EditableAnswers[j].Text))
                    {
                        return BadRequest("Bad answer text");
                    }

                    _dataContext.EditableAnswers.Add(new EditableAnswer()
                    {
                        EditableQuestionId = editableQuestions[i].Id,
                        Text = poll.Questions[i].EditableAnswers[j].Text
                    });
                }
            }

            await _dataContext.SaveChangesAsync();

            return Ok(_dataContext.EditablePolls.Include(x=>x.Questions).ThenInclude(y=> y.EditableAnswers).Where(z=> z.Id == editablePoll.Id));
        }

        [HttpPost("EditPoll")]
        public async Task<ActionResult<string>> EditPoll(GeneralPoll poll)
        {

            GeneralPoll generalPoll = await _dataContext.GeneralPolls.Include(x=>x.Questions)
                .ThenInclude(y=> y.GeneralAnswers).FirstAsync(x => x.Id == poll.Id);

            generalPoll.Name = poll.Name;

            await _dataContext.SaveChangesAsync();

            List<GeneralQuestion> generalQuestions = _dataContext.GeneralQuestions.Where(x => x.GeneralPollId == poll.Id).ToList();

            if(generalQuestions.Count > poll.Questions.Count)
            {
                for(int i=0; i<poll.Questions.Count; i++) 
                {
                    generalQuestions[i].Name = poll.Questions[i].Name;
                }

                generalQuestions.RemoveRange(poll.Questions.Count, generalQuestions.Count - 1);
            }
            else if(generalQuestions.Count < poll.Questions.Count)
            {
                for (int i = 0; i<generalQuestions.Count; i++)
                {
                    generalQuestions[i].Name = poll.Questions[i].Name;
                }

                for(int i = generalQuestions.Count; i < poll.Questions.Count; ++i)
                {
                    _dataContext.GeneralQuestions.Add(new GeneralQuestion()
                    {
                        Name = generalQuestions[i].Name,
                        GeneralPollId = poll.Questions[i].GeneralPollId,
                    });
                }
            }
            else
            {
                for (int i = 0; i<generalQuestions.Count; i++)
                {
                    generalQuestions[i].Name = poll.Questions[i].Name;
                }
            }

            await _dataContext.SaveChangesAsync();

            generalQuestions = _dataContext.GeneralQuestions.Where(x => x.GeneralPollId == poll.Id).ToList();

            for (int i=0; i<generalQuestions.Count; ++i)
            {
                List<GeneralAnswer> answers = _dataContext.GeneralAnswers.Where(x=> x.GeneralQuestionId == generalQuestions[i].Id).ToList();

                if(answers.Count > poll.Questions[i].GeneralAnswers.Count)
                {
                    for(int j=0; j<poll.Questions[i].GeneralAnswers.Count; ++j)
                    {
                        answers[j].Text = poll.Questions[i].GeneralAnswers[j].Text;
                        answers[j].GeneralQuestionId = generalQuestions[i].Id;
                    }

                    answers.RemoveRange(poll.Questions[i].GeneralAnswers.Count, answers.Count - 1);

                }else if(answers.Count < poll.Questions[i].GeneralAnswers.Count)
                {
                    for (int j = 0; j<answers.Count; ++j)
                    {
                        answers[j].Text = poll.Questions[i].GeneralAnswers[j].Text;
                        answers[j].GeneralQuestionId = generalQuestions[i].Id;
                    }
                    for(int j=answers.Count; j<poll.Questions[i].GeneralAnswers.Count; ++j)
                    {
                        _dataContext.GeneralAnswers.Add(new GeneralAnswer()
                        {
                            Text = poll.Questions[i].GeneralAnswers[j].Text,
                            GeneralQuestionId = poll.Questions[i].GeneralAnswers[j].GeneralQuestionId
                        });
                    }
                }
                else
                {
                    for (int j = 0; j < poll.Questions[i].GeneralAnswers.Count; ++j)
                    {
                        answers[j].Text = poll.Questions[i].GeneralAnswers[j].Text;
                        answers[j].GeneralQuestionId = generalQuestions[i].Id;
                    }
                }
            }

            await _dataContext.SaveChangesAsync();

            generalPoll = _dataContext.GeneralPolls.Include(x => x.Questions).ThenInclude(y=> y.GeneralAnswers).First(z=> z.Id == generalPoll.Id);

            return Ok(generalPoll);
        }

        [HttpPost("DeletePoll")]
        public async Task<ActionResult<string>> DeletePoll(int pollId)
        {
            GeneralPoll poll =  _dataContext.GeneralPolls.Include(x=> x.Questions)
                .ThenInclude(y=>y.GeneralAnswers).First(z=> z.Id == pollId);

            var questions = poll.Questions;

            List<GeneralAnswer> generalAnswers = new List<GeneralAnswer>();

            foreach (var question in questions)
            {
                generalAnswers.AddRange(question.GeneralAnswers);
            }

            foreach (var generalAnswer in generalAnswers)
            {
                _dataContext.GeneralAnswers.Remove(generalAnswer);
            }

            foreach(GeneralQuestion generalQuestion in questions)
            {
                _dataContext.GeneralQuestions.Remove(generalQuestion);
            }

            if(poll != null)
            _dataContext.GeneralPolls.Remove(poll);
            else
                return BadRequest("Bad poll Id");


            await _dataContext.SaveChangesAsync();

            return Ok(_dataContext.GeneralPolls);
        }

        [HttpGet("GetDoctorPolls")]
        public async Task<ActionResult<string>> GetDoctorPolls(int doctorId)
        {
            
            return Ok(_dataContext.GeneralPolls.Include(x => x.Questions).ThenInclude(y => y.GeneralAnswers).Where(x=>x.DoctorId == doctorId));
        }

        [HttpGet("GetPatientsPoll")]
        public async Task<ActionResult<string>> GetPatientsPoll(int patientId)
        {
            var pollsWithEmails = _dataContext.EditablePolls.Join(_dataContext.Patients, a => a.PatientId, b => b.Id, 
                (poll, patient) => new { poll, patient = patient.Email });
            return Ok(_dataContext.EditablePolls.Include(x => x.Questions).ThenInclude(y => y.EditableAnswers).Include(x=>x.Comments)
               .Join(_dataContext.Patients, a => a.PatientId, b => b.Id,
                (poll, patient) => new { poll, patient = patient.Email }).Where(z => z.poll.PatientId == patientId));
        }


        [HttpGet("GetEditPollId")]
        public async Task<ActionResult<string>> GetEditPollId(int patientId, string pollName)
        {
            return Ok(_dataContext.EditablePolls.Where(x => x.Name.Equals(pollName) && x.PatientId == patientId).Select(y=> y.Id));
        }

        [HttpGet("GetPollPatients")]
        public async Task<ActionResult<string>> GetAllPollPatients(string pollName)
        {
            List<int> patientIds = _dataContext.EditablePolls.Where(x => x.Name.Equals(pollName)).Select(y => y.PatientId).ToList();

            return Ok(_dataContext.Patients.Where(x => patientIds.Contains(x.Id)));
        }

        [HttpGet("GetPollById")]
        public async Task<ActionResult<string>> GetPollById(int pollId)
        {
            return Ok(_dataContext.GeneralPolls.Include(x=>x.Questions).ThenInclude(y=>y.GeneralAnswers).Where(x => x.Id == pollId));
        }
    }
}
