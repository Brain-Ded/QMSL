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
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;

        public UserController(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _config = configuration;
        }


        [HttpGet("GetPatients")]
        public async Task<ActionResult<string>> GetDoctorPatients(int doctorId)
        {
            return Ok(_dataContext.Patients.Include("Polls").Include("Doctors").Select(x => x.Doctors.Where(y => y.Id == doctorId)));
        }

        [HttpGet("GetDoctors")]
        public async Task<ActionResult<string>> GetDoctorDoctors(int patientId)
        {
            return Ok(_dataContext.Doctors.Include("Polls").Include("Patients").Select(x => x.Patients.Where(y => y.Id == patientId)));
        }

        [HttpGet("GetAllPatients")]
        public async Task<ActionResult<string>> GetAllPatients()
        {
            return Ok(_dataContext.Patients.Include("Polls").Include("Doctors").Select(x => x.Doctors));
        }

        [HttpGet("GetAllDoctors")]
        public async Task<ActionResult<string>> GetAllDoctors()
        {
            return Ok(_dataContext.Doctors.Include("Polls").Include("Patients").Select(x => x.Patients));
        }
    }
}
