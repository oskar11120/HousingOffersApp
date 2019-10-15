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
            int? neededId = repozitory.GetUserID(user);
            if (neededId != null)
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

        [HttpPatch("update")]
        public IActionResult Update([FromBody] UserModel user)
        {
            ///TODO add user validation
            bool valid = true;

            if (valid)
            {
                repozitory.UpdateUser(user);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            //TODO add request validation
            bool valid = true;

            if(valid)
            {
                repozitory.DeleteUser(userId);   
            }
        }
    }
}