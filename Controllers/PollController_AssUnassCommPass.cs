using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QMSL.Dtos;
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

        [HttpPost("Assign")]
        public async Task<ActionResult<string>> AssignPoll(PollDto poll, string patientEmail)
        {
            if(!await _dataContext.Patients.AnyAsync(x => x.Email.Equals(patientEmail)))
            {
                return BadRequest("Patient with this email does not exists");
            }

            var patient = _dataContext.Patients.Include("Polls").First(x => x.Email == patientEmail);

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

        [HttpPost("Unassign")]
        public async Task<ActionResult<string>> UnassignPoll(PollDto poll, string patientEmail)
        {
            if (!await _dataContext.Patients.AnyAsync(x => x.Email.Equals(patientEmail)))
            {
                return BadRequest("Patient with this email does not exists");
            }

            var patient = _dataContext.Patients.Include("Polls").First(x => x.Email == patientEmail);

            if (!patient.Polls.Any(x => x.Name == poll.Name))
            {
                return BadRequest("Patient with this email doesn't has this poll");
            }

            var editPoll = _dataContext.EditablePolls.FirstOrDefault(x => x.Name == poll.Name);
            _pollsService.UnassignPoll(patient, editPoll.Id);
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

            var patient = _dataContext.Patients.First(x => x.Id == patientId);
            if(await _dataContext.Doctors.AnyAsync(x => x.Patients.Contains(patient)))
            {
                return BadRequest("This patient already assigned to this doctor");
            }

            var doctor = _dataContext.Doctors.Include("Patients").First(x => x.Id == doctorId);

            doctor.Patients.Add(patient);
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

            var patient = _dataContext.Patients.First(x => x.Id == patientId);
            if (!_dataContext.Doctors.Include("Patients").First(x => x.Id == doctorId).Patients.Contains(patient))
            {
                return BadRequest("This patient is not assigned to this doctor");
            }

            var doctor = _dataContext.Doctors.First(x => x.Id == doctorId);

            if (doctor.Patients == null)
                doctor.Patients = new List<Models.Patient>();

            doctor.Patients.Remove(patient);
            await _dataContext.SaveChangesAsync();

            return Ok(doctor);
        }

        //[HttpPost]
        //public async Task<ActionResult<string>> CommentPoll(PollDto, )
    }
}
