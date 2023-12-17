using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QMSL.Dtos;
using QMSL.Services;

namespace QMSL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration? _config;

        public StatisticController(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _config = config;
        }

        [HttpGet("GetPatientPollStatistic")]
        public async Task<ActionResult<string>> GetPatientPollStatistic(int patientId)
        {
            if (!await _dataContext.Patients.AnyAsync(x => x.Id == patientId))
            {
                return BadRequest("Patient with this id is not exists");
            }

            var patient = _dataContext.Patients.Include(x => x.Polls).First(x => x.Id == patientId);
            StatisticDto statistic = new StatisticDto();
            statistic.TotalPollAmount = patient.Polls.Count;
            statistic.PassedPollAmount = patient.Polls.Where(x => x.IsPassed).Count();
            statistic.NotPassedPollAmount = statistic.TotalPollAmount - statistic.PassedPollAmount;

            return Ok(statistic);
        }

        [HttpGet("GetPatientPollStatisticAt")]
        public async Task<ActionResult<string>> GetPatientPollStatisticAt(int patientId, DateTime from, DateTime to)
        {
            if (!await _dataContext.Patients.AnyAsync(x => x.Id == patientId))
            {
                return BadRequest("Patient with this id is not exists");
            }

            var patient = _dataContext.Patients.Include("Polls").First(x => x.Id == patientId);
            StatisticDto statistic = new StatisticDto();
            statistic.TotalPollAmount = patient.Polls.Where(x => x.AssignedAt > from && x.AssignedAt < to).Count();
            statistic.PassedPollAmount = patient.Polls.Where(x => x.IsPassed && x.AssignedAt > from && x.AssignedAt < to).Count();
            statistic.NotPassedPollAmount = statistic.TotalPollAmount - statistic.PassedPollAmount;

            return Ok(statistic);
        }

        [HttpGet("GetDoctorStatistic")]
        public async Task<ActionResult<string>> GetDoctorStatistic(int doctorId)
        {
            if(!await _dataContext.Doctors.AnyAsync(x => x.Id == doctorId))
            {
                return BadRequest("Doctor with this id is not exists");
            }

            var doctor = _dataContext.Doctors.Include("Polls").First(x => x.Id == doctorId);
            StatisticDto statistic = new StatisticDto();
            doctor.Polls.ForEach(x =>
            {
                statistic.TotalPollAmount = _dataContext.Patients.Include("Polls").Where(y => y.Polls.Any(z => z.Name == x.Name)).Count();
                statistic.PassedPollAmount = _dataContext.Patients.Include("Polls").Where(y => y.Polls.Any(z => z.Name == x.Name && z.IsPassed)).Count();
                statistic.NotPassedPollAmount = statistic.TotalPollAmount - statistic.PassedPollAmount;
            });

            return Ok(statistic);
        }

        [HttpGet("GetDoctorStatisticAt")]
        public async Task<ActionResult<string>> GetDoctorStatisticAt(int doctorId, DateTime from, DateTime to)
        {
            if (!await _dataContext.Doctors.AnyAsync(x => x.Id == doctorId))
            {
                return BadRequest("Doctor with this id is not exists");
            }

            var doctor = _dataContext.Doctors.Include("Polls").First(x => x.Id == doctorId);
            StatisticDto statistic = new StatisticDto();
            doctor.Polls.ForEach(x =>
            {
                statistic.TotalPollAmount = _dataContext.Patients.Include("Polls").Where(y => y.Polls.Any(z => z.Name == x.Name && z.AssignedAt > from && z.AssignedAt < to)).Count();
                statistic.PassedPollAmount = _dataContext.Patients.Include("Polls").Where(y => y.Polls.Any(z => z.Name == x.Name && z.IsPassed && z.AssignedAt > from && z.AssignedAt < to)).Count();
                statistic.NotPassedPollAmount = statistic.TotalPollAmount - statistic.PassedPollAmount;
            });

            return Ok(statistic);
        }
    }
}
