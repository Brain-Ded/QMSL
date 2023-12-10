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
        public async Task<ActionResult<string>> CreatePoll(GeneralQuestionDto poll, string DoctorEmail)
        {


            return Ok();
        }
    }
}
