using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HousingOffersAPI.Models;
using HousingOffersAPI.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HousingOffersAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserCreationValidator userCreationValidator; //TODO constructor injection

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel user)
        {
            if (!userCreationValidator.isValid(user)) return BadRequest("Invalid register credentials!");
            else
            {
                //TODO adding user to database
            }
            throw new NotImplementedException();
        }
    }
}