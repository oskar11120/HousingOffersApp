using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HousingOffersAPI.Models;
using HousingOffersAPI.Services;
using HousingOffersAPI.Services.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HousingOffersAPI.Controllers
{
    [Authorize]
    [Route("api/offers/")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        public OffersController(IOffersRepozitory repozitory, IJwtManager jwtManager, IOfferValidator offerValidator, IOfferGetRequestValidator offerGetRequestValidator)
        {
            this.repozitory = repozitory;
            this.jwtManager = jwtManager;
            this.offerValidator = offerValidator;
            this.offerGetRequestValidator = offerGetRequestValidator;
        }

        private readonly IOffersRepozitory repozitory;
        private readonly IJwtManager jwtManager;
        private readonly IOfferValidator offerValidator;
        private readonly IOfferGetRequestValidator offerGetRequestValidator;

        // returns offers for given getOffersInput
        [AllowAnonymous]
        [HttpGet("{offerId}")]
        public IActionResult GetOffer(int offerId)
        {
            var outputOffer = repozitory.GetOffer(offerId);
            if (outputOffer == null)
                return BadRequest("No such offer!");
            else
                outputOffer.User.Password = "";

            return Ok(AutoMapper.Mapper.Map<Entities.Offer, Models.OfferModel>(outputOffer));
        }

        [AllowAnonymous]
        [HttpPost("get")]
        public IActionResult GetOffers([FromBody] OffersRequestContentModel requestContentModel)
        {
            var error = offerGetRequestValidator.IsRequestValid(requestContentModel);
            if (error != null)
                return BadRequest(error);

            var offerEntities = repozitory.GetOffers(requestContentModel).ToList();
            var output = repozitory.GetOffers(requestContentModel)
                .Select(offerEntity => AutoMapper.Mapper.Map<Entities.Offer, Models.OfferModel>(offerEntity))
                .ToList();
            for (int i = 0; i < output.Count(); i++)
            {
                output[i].User.Password = null;
            }
            return Ok(output);
        }

        // adds new offer to database
        [HttpPost("add")]
        public IActionResult AddOffer([FromBody] OfferModel createOfferInput)
        {
            var error = offerValidator.IsOfferValid(createOfferInput);
            if (error != null)
                return BadRequest(error);

            createOfferInput.User = null;
            createOfferInput.UserId = jwtManager.GetClaimOfType(User.Claims.ToArray(), "UserId");

            repozitory.AddOffer(createOfferInput);
            return Ok();
        }

        // deletes offert that correspons to given id
        [HttpDelete("{offerId}")]
        public IActionResult DeleteOffer(int offerId)
        {
            //TODO validation
            if (!jwtManager.IsClaimValidToRequestedOfferId(offerId, User.Claims.ToArray()))
                return Unauthorized();

            repozitory.DeleteOffer(offerId);
            return Ok();
        }

        [HttpPatch("update")]
        public IActionResult UpdateOffer([FromBody] OfferModel offer)
        {
            if (!jwtManager.IsClaimValidToRequestedOfferId(offer.Id, User.Claims.ToArray()))
                return Unauthorized();

            //TODO request validation
            repozitory.UpdateOffer(offer);
            return Ok();
        }
    }
}
