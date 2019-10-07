using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HousingOffersAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DataController : ControllerBase
    {
        // returns offers for given getOffersInput
        [HttpPost("getOffers")]
        public void GetOffers([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // adds new offer to database
        [HttpPost("newOffer")]
        public void AddOffer([FromBody] string createOfferInput)
        {
            throw new NotImplementedException();
        }

        // deletes off that correspons to given id
        [HttpDelete("offerId")]
        public void DeleteOffer()
        {
            throw new NotImplementedException();
        }
    }
}
