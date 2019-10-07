using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HousingOffersAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [Route("login")]
        public void Login([FromBody] string user)
        {
            throw new NotImplementedException();
        }

        [Route("register")]
        public void Register([FromBody] string user)
        {
            throw new NotImplementedException();
        }
    }
}