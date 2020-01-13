using HousingOffersAPI.Models.AnalyticsRelated;
using HousingOffersAPI.Services.AnalyticsRelated;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Controllers
{
    [ApiController]
    [Route("api/analytics/")]
    public class DataCollectionController : ControllerBase
    {
        public DataCollectionController(IAnalyticsDataRepozitory analyticsDataRepozitory)
        {
            this.analyticsDataRepozitory = analyticsDataRepozitory;
        }

        private readonly IAnalyticsDataRepozitory analyticsDataRepozitory;

        [HttpPost("offerOpened")]
        public IActionResult OfferWasOpened(UserOfferRequestAnalyticsModel userOpenedOfferAnalyticsModel)
        {
            analyticsDataRepozitory.AddOfferRequest(userOpenedOfferAnalyticsModel);
            return Ok();
        }


        [HttpPost("offerOpened")]
        public IActionResult UserContactWasRequested(UserContactRequestAnalyticsModel userOpenContactAnalyticsModel)
        {
            analyticsDataRepozitory.AddContactRequest(userOpenContactAnalyticsModel);
            return Ok();
        }

    }
}
