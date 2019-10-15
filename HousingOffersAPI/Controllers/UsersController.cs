using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HousingOffersAPI.Models;
using HousingOffersAPI.Services.UsersRelated;
using HousingOffersAPI.Services.Validators;
using HousingOffersAPI.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HousingOffersAPI.Controllers
{
    [Authorize]
    [Route("api/users/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(IUserValidator userCreationValidator, IUsersRepozitory repozitory, IJwtManager jwtManager)
        {
            this.userCreationValidator = userCreationValidator;
            this.repozitory = repozitory;
            this.jwtManager = jwtManager;
        }

        private readonly IUserValidator userCreationValidator;
        private readonly IUsersRepozitory repozitory;
        private readonly IJwtManager jwtManager;

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            ///add here validiation and sanitization
            var neededUserId = repozitory.GetUserID(user);
            if (neededUserId != null)
            {
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtManager.CreateJWT((int)neededUserId))
                });
            }
            return BadRequest("Could not verify username and password");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel user)
        {
            if (!userCreationValidator.IsNewUserValid(user)) return BadRequest("Invalid register credentials!");
            else
            {
                repozitory.AddUser(user);
                return Ok();
            }
        }

        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            var outputUser = repozitory.GetUser(userId);
            if (outputUser == null)
            {
                return BadRequest("User does not exist!");
            }              
            else
            {
                return Ok(AutoMapper.Mapper.Map<Entities.User, Models.UserModel>(outputUser));
            }             
        }

        [HttpPatch("update")]
        public IActionResult Update([FromBody] UserUpdateRequestContentModel userUpdateContent)
        {
            ///TODO add user validation
            int? userId = repozitory.GetUserID(new UserModel()
            {
                Email = userUpdateContent.Email,
                Login = userUpdateContent.Login,
                Password = userUpdateContent.Password
            });

            if (userId == null)
                return BadRequest("User not found!");
            if(!jwtManager.IsClaimValidToRequestedUserId((int)userId, User.Claims.ToArray()))
                return Unauthorized();

            repozitory.UpdateUser(new UserModel()
            {
                Login = userUpdateContent.LoginNew,
                Password = userUpdateContent.PasswordNew,
                PhoneNumber = userUpdateContent.PhoneNumberNew
            }, (int)userId);
            return Ok();         
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            //TODO add request validation
            if (!jwtManager.IsClaimValidToRequestedUserId((int)userId, User.Claims.ToArray()))
                return Unauthorized();

            repozitory.DeleteUser(userId);
            return Ok();
        }
    }
}