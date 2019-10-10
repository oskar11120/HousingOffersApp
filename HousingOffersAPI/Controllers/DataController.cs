using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HousingOffersAPI.Models;
using HousingOffersAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HousingOffersAPI.Controllers
{
    [Route("api/offers/")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public DataController(IOffersRepozitory repozitory)
        {
            this.repozitory = repozitory;
        }

        private readonly IOffersRepozitory repozitory;

        // returns offers for given getOffersInput
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
        public void AddOffer([FromBody] OfferModel createOfferInput)
        {
            repozitory.AddOffer(createOfferInput);
        }

        // deletes off that correspons to given id
        [HttpDelete("{offerId}")]
        public void DeleteOffer()
        {
            throw new NotImplementedException();
        }
    }
}
