using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HousingOffersAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HousingOffersAPI.Controllers
{
    [Route("api/dummy")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        public DummyController(HousingOffersContext context)
        {
            this.context = context;
        }

        private HousingOffersContext context;

        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}