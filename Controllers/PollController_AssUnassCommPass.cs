using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QMSL.Dtos;
using QMSL.Models;
using QMSL.Services;
using System.Runtime.CompilerServices;

namespace QMSL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController_AssUnassCommPass : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;
        private readonly PollsService _pollsService;

        public PollController_AssUnassCommPass(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _config = config;
            _pollsService = new PollsService();
        }

        [HttpPost("Assign Poll to Patient")]
        public async Task<ActionResult<string>> AssignPoll(string pollName, string patientEmail)
        {
            if(!await _dataContext.Patients.AnyAsync(x => x.Email.Equals(patientEmail)))
            {
                return BadRequest("Patient with this email does not exists");
            }

            if(!await _dataContext.GeneralPolls.AnyAsync(x => x.Name == pollName))
            {
                return BadRequest("Poll with this name does not exists");
            }

            var patient = _dataContext.Patients.Include("Polls").First(x => x.Email == patientEmail);
            var poll = _dataContext.GeneralPolls.First(x => x.Name == pollName);

            if(patient.Polls.Any(x => x.Name == poll.Name))
            {
                return BadRequest("Patient with this email already has this poll");
            }

            var editablePoll = _dataContext.GeneralPolls.First(x => x.Name == poll.Name).getEditCopy();
            _dataContext.EditablePolls.Add(editablePoll);

            _pollsService.AssignPoll(patient, editablePoll);
            await _dataContext.SaveChangesAsync();

            return Ok(patient);
        }

        [HttpPost("Unassign Poll from Patient")]
        public async Task<ActionResult<string>> UnassignPoll(string pollName, string patientEmail)
        {
            if (!await _dataContext.Patients.AnyAsync(x => x.Email.Equals(patientEmail)))
            {
                return BadRequest("Patient with this email does not exists");
            }

            if(!await _dataContext.EditablePolls.AnyAsync(x => x.Name == pollName))
            {
                return BadRequest("Poll with this name does not exists");
            }

            var patient = _dataContext.Patients.Include("Polls").First(x => x.Email == patientEmail);
            var poll = _dataContext.EditablePolls.First(x => x.Name == pollName);

            if (!patient.Polls.Any(x => x.Name == poll.Name))
            {
                return BadRequest("Patient with this email doesn't has this poll");
            }

            _pollsService.UnassignPoll(patient, poll.Id);
            _dataContext.EditablePolls.Remove(poll);
            await _dataContext.SaveChangesAsync();

            return Ok(patient);
        }

        [HttpPost("Assign Patient")]
        public async Task<ActionResult<string>> AssignPatient(int patientId, int doctorId)
        {
            if(!await _dataContext.Patients.AnyAsync(x => x.Id.Equals(patientId)))
            {
                return BadRequest("Patient with this id does not exists");
            }

            if (!await _dataContext.Doctors.AnyAsync(x => x.Id.Equals(doctorId)))
            {
                return BadRequest("Doctor with this id does not exists");
            }

            var patient = _dataContext.Patients.Include("Doctors").First(x => x.Id == patientId);
            if (_dataContext.Doctors.Include(d => d.Patients).First(d => d.Id == doctorId).Patients.Contains(patient))
            {
                return BadRequest("This patient already assigned to this doctor");
            }

            var doctor = _dataContext.Doctors.Include("Patients").First(x => x.Id == doctorId);

            doctor.Patients.Add(patient);
            patient.Doctors.Add(doctor);
            await _dataContext.SaveChangesAsync();

            return Ok(doctor);
        }

        [HttpPost("Unassign Patient")]
        public async Task<ActionResult<string>> UnassignPatient(int patientId, int doctorId)
        {
            if (!await _dataContext.Patients.AnyAsync(x => x.Id.Equals(patientId)))
            {
                return BadRequest("Patient with this id does not exists");
            }

            if (!await _dataContext.Doctors.AnyAsync(x => x.Id.Equals(doctorId)))
            {
                return BadRequest("Doctor with this id does not exists");
            }

            var patient = _dataContext.Patients.Include("Doctors").First(x => x.Id == patientId);
            if (!_dataContext.Doctors.Include("Patients").First(x => x.Id == doctorId).Patients.Contains(patient))
            {
                return BadRequest("This patient is not assigned to this doctor");
            }

            var doctor = _dataContext.Doctors.First(x => x.Id == doctorId);

            if (doctor.Patients == null)
                doctor.Patients = new List<Models.Patient>();

            doctor.Patients.Remove(patient);
            patient.Doctors.Remove(doctor);
            await _dataContext.SaveChangesAsync();

            return Ok(doctor);
        }

        [HttpPost("Comment Poll")]
        public async Task<ActionResult<string>> CommentPoll(int pollId, CommentDto commentDto)
        {
            if(!await _dataContext.EditablePolls.AnyAsync(x => x.Id.Equals(pollId)))
            {
                return BadRequest("Poll with this id is not exists");
            }

            if(!await _dataContext.Doctors.AnyAsync(x => x.Id == commentDto.DoctorId))
            {
                return BadRequest("Doctor with this id is not exists");
            }

            var dbPoll = _dataContext.EditablePolls.Include("Comments").First(x => x.Id == pollId);
            Comment comment = new Comment() { Text = commentDto.Text, DoctorId = commentDto.DoctorId, type = commentDto.type, CommentedAt = DateTime.Now };

            var commentToAdd = _dataContext.Comments.Add(comment);
            _pollsService.CommentPoll(dbPoll, commentToAdd.Entity);
            await _dataContext.SaveChangesAsync();

            return Ok(dbPoll);
        }

        [HttpPost("Pass Poll")]
        public async Task<ActionResult<string>> PassPoll(int pollId)
        {
            if(!await _dataContext.EditablePolls.AnyAsync(x => x.Id == pollId))
            {
                return BadRequest("Poll with this id is not exists");
            }

            var poll = _dataContext.EditablePolls.First(x => x.Id == pollId);

            if(poll.IsPassed)
            {
                return Ok("Poll is already passed");
            }

            _pollsService.PassPoll(poll);
            await _dataContext.SaveChangesAsync();

            return Ok(poll);
        }
    }
}
