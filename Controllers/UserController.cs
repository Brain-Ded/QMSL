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


        [HttpGet("GetPatientDoctors")]
        public async Task<ActionResult<string>> GetDoctorPatients(int patientId)
        {
            return Ok(_dataContext.Patients.Where(x => x.Id == patientId).Include("Polls").Include("Doctors"));
        }

        [HttpGet("GetDoctorPatients")]
        public async Task<ActionResult<string>> GetPatientDoctors(int doctorId)
        {
            return Ok(_dataContext.Doctors.Where(x => x.Id == doctorId).Include("Polls").Include("Patients"));
        }

        [HttpGet("GetDoctorById")]
        public async Task<ActionResult<string>> GetDoctorById(int doctorId)
        {
            return Ok(_dataContext.Doctors.FindAsync(doctorId));
        }
        [HttpGet("GetPatientById")]
        public async Task<ActionResult<string>> GetPatientById(int patientId)
        {
            return Ok(_dataContext.Patients.FindAsync(patientId));
        }

        [HttpGet("GetAllPatients")]
        public async Task<ActionResult<string>> GetAllPatients()
        {
            return Ok(_dataContext.Patients.ToList());
        }

        [HttpGet("GetAllDoctors")]
        public async Task<ActionResult<string>> GetAllDoctors()
        {
            return Ok(_dataContext.Doctors.ToList());
        }
    }
}
