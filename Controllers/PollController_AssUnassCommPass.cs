﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<ActionResult<string>> AssignPoll(PollDto poll, string patientEmail)
        {
            if(!await _dataContext.Patients.AnyAsync(x => x.Email.Equals(patientEmail)))
            {
                return BadRequest("Patient with this email does not exists");
            }

            var patient = _dataContext.Patients.First(x => x.Email == patientEmail);

            if ((bool)(patient.Polls?.Any(x => x.Name == poll.Name)))
            {
                return BadRequest("Patient with this email already has this poll");
            }

            _pollsService.AssignPoll(patient, _dataContext.GeneralPolls.FirstOrDefault(x => x.Name == poll.Name)?.getEditCopy());

            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult<string>> UnassignPoll(PollDto poll, string patientEmail)
        {
            if (!await _dataContext.Patients.AnyAsync(x => x.Email.Equals(patientEmail)))
            {
                return BadRequest("Patient with this email does not exists");
            }

            var patient = _dataContext.Patients.First(x => x.Email == patientEmail);

            if (!(bool)(patient.Polls?.Any(x => x.Name == poll.Name)))
            {
                return BadRequest("Patient with this email doesn't has this poll");
            }

            var editPoll = patient.Polls.FirstOrDefault(x => x.Name == poll.Name);
            _pollsService.UnassignPoll(patient, editPoll.Id);

            return Ok(patient);
        }

        //[HttpPost]
        //public async Task<ActionResult<string>> CommentPoll(PollDto, )
    }
}
