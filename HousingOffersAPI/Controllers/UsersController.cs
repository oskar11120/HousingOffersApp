using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HousingOffersAPI.Models;
using HousingOffersAPI.Services.UsersRelated;
using HousingOffersAPI.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HousingOffersAPI.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(IUserCreationValidator userCreationValidator, IUsersRepozitory repozitory)
        {
            this.userCreationValidator = userCreationValidator;
            this.repozitory = repozitory;
        }

        private readonly IUserCreationValidator userCreationValidator;
        private readonly IUsersRepozitory repozitory;

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            if (repozitory.DoesUserExist(user))
            {
                ////TODO handle creation and sending of JWT
            }
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel user)
        {
            if (!userCreationValidator.isValid(user)) return BadRequest("Invalid register credentials!");
            else
            {
                repozitory.AddUser(user);
                return Ok();
            }
        }
    }
}