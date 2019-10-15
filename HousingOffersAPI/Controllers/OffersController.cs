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
        public OffersController(IOffersRepozitory repozitory, IJwtManager jwtManager)
        {
            this.repozitory = repozitory;
            this.jwtManager = jwtManager;
        }

        private readonly IOffersRepozitory repozitory;
        private readonly IJwtManager jwtManager;

        // returns offers for given getOffersInput
        [AllowAnonymous]
        [HttpGet("{offerId}")]
        public IActionResult GetOffer(int offerId)
        {
            var outputOffer = repozitory.GetOffer(offerId);
            if (outputOffer == null)
                return BadRequest("No such offer!");
            else
                return Ok(AutoMapper.Mapper.Map<Entities.Offer, Models.OfferModel>(outputOffer));
        }

        [AllowAnonymous]
        [HttpPost("get")]
        public IActionResult GetOffers([FromBody] OffersRequestContentModel requestContentModel)
        {
            var offerEntities = repozitory.GetOffers(requestContentModel).ToList();
            var output = repozitory.GetOffers(requestContentModel)
                .Select(offerEntity => AutoMapper.Mapper.Map<Entities.Offer, Models.OfferModel>(offerEntity))
                .ToList();
            for (int i = 0; i < output.Count(); i++)
            {
                output[i].Images = offerEntities[i].Images
                    .Select(imageEtity => AutoMapper.Mapper.Map<Entities.ImageAdress, Models.ImageAdressModel>(imageEtity));
                output[i].OfferTags = offerEntities[i].OfferTags
                    .Select(offerTagEntity => AutoMapper.Mapper.Map<Entities.OfferTag, Models.OfferTagModel>(offerTagEntity));
            }
            return Ok(output);
        }

        // adds new offer to database
        [HttpPost("add")]
        public IActionResult AddOffer([FromBody] OfferModel createOfferInput)
        {
            createOfferInput.UserId = jwtManager.getClaimedUserId(User.Claims.ToArray());

            repozitory.AddOffer(createOfferInput);
            return Ok();
        }

        // deletes off that correspons to given id
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
