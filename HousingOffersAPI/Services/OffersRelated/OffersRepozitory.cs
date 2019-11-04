using Geolocation;
using HousingOffersAPI.Entities;
using HousingOffersAPI.Models;
using HousingOffersAPI.Models.DatabaseRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HousingOffersAPI.Services
{    
    public class OffersRepozitory : IOffersRepozitory
    {
        public OffersRepozitory(HousingOffersContext context)
        {
            this.context = context;
        }
        
        private HousingOffersContext context;

        public Offer GetOffer(int offerId)
        {
            var listWithNeededOffer = context.Offers.Where(offer => offer.Id == offerId)
                .Include(offer => offer.Images)
                .Include(offer => offer.OfferTags)
                .Include(offer => offer.User)
                .ToList();
            if (listWithNeededOffer.Count == 0)
                return null;
            else
                return listWithNeededOffer[0];
        }

        public List<Offer> GetOffers(OffersRequestContentModel offersRequestContentModel)
        {
            //TODO handle querying for location
            var query = context.Offers.Where(offer => true);

            if(offersRequestContentModel.OfferId != null)
            {
                query = query.Where(offer => offer.Id == offersRequestContentModel.OfferId);
            }
            if (offersRequestContentModel.UserId != null)
            {
                query = query.Where(offer => offer.UserId == offersRequestContentModel.UserId);
            }
            if (offersRequestContentModel.PriceLimits != null)
            {
                query = query.Where(offer => offer.PriceInPLN >= offersRequestContentModel.PriceLimits[0]
                && offer.PriceInPLN <= offersRequestContentModel.PriceLimits[1]);
            }                    
            if (offersRequestContentModel.AreaLimits != null)
            {
                query = query.Where(offer => offer.Area >= offersRequestContentModel.AreaLimits[0]
                && offer.Area <= offersRequestContentModel.AreaLimits[1]);
            }
            if(offersRequestContentModel.PropertyTypes != null)
            {
                query = query.Where(offer => offersRequestContentModel.PropertyTypes
                .Any(propertyType => propertyType == offer.PropertyType));
            }
            if (offersRequestContentModel.OfferTypes != null)
            {
                query = query.Where(offer => offersRequestContentModel.OfferTypes
                .Any(offerType => offerType == offer.OfferType));
            }
            if(offersRequestContentModel.DateTimeLimits != null)
            {
                query = query.Where(offer => offer.CreationDate >= offersRequestContentModel.DateTimeLimits[0]
                && offer.CreationDate <= offersRequestContentModel.DateTimeLimits[1]);
            }

            //including objects nested in offers
            query = query
                .Include(offer => offer.Images)
                .Include(offer => offer.OfferTags)
                .Include(offer => offer.User)
                .Include(offer => offer.Location);

            List<Offer> offersOutput = query.ToList();

            //query against locations
            if (offersRequestContentModel.Location != null && offersRequestContentModel.MaxDistanceFromLocation > 0)
            {
                offersOutput = getOfferWithinSpecifiedDistance(offersRequestContentModel.Location, offersRequestContentModel.MaxDistanceFromLocation, offersOutput)
                    .ToList();
            }
            return offersOutput;
        }

        public void AddOffer(OfferModel offer)
        {
            offer.CreationDate = DateTime.Now;
            context.Offers.Add(AutoMapper.Mapper.Map<Models.OfferModel, Entities.Offer>(offer));
            context.SaveChanges();
        }

        public void DeleteOffer(int offerId)
        {
            var offerToDelete = context.Offers.SingleOrDefault(offer => offer.Id == offerId);
            if (offerToDelete != null)
            {
                context.Offers.Remove(offerToDelete);
                context.SaveChanges();
            }            
        }

        public void UpdateOffer(OfferModel offerUpdater)
        {
            var offerToUpdate = context.Offers.SingleOrDefault(offerEntity => offerEntity.Id == offerUpdater.Id);
            if (offerToUpdate == null) return;

            if (offerUpdater.OfferType != null) offerToUpdate.OfferType = offerUpdater.OfferType;
            if (offerUpdater.PriceInPLN != 0) offerToUpdate.PriceInPLN = offerUpdater.PriceInPLN;
            if (offerUpdater.PropertyType != null) offerToUpdate.PropertyType = offerUpdater.PropertyType;
            if (offerUpdater.Location != null)
            {
                if (offerUpdater.Location.Lattitue != 0)
                    offerToUpdate.Location.Lattitue = offerUpdater.Location.Lattitue;
                if (offerUpdater.Location.Longitude != 0)
                    offerToUpdate.Location.Longitude = offerUpdater.Location.Longitude;
                if (offerUpdater.Location.Description != null)
                    offerToUpdate.Location.Description = offerToUpdate.Location.Description;
            } 
            if (offerUpdater.Area != 0) offerToUpdate.Area = offerUpdater.Area;
            if (offerUpdater.Description != null) offerToUpdate.Description = offerUpdater.Description;
            if (offerUpdater.Images != null && offerUpdater.Images.Count() != 0)
            {
                foreach (var image in offerUpdater.Images)
                {
                    var referenceImageEntity = offerToUpdate.Images.SingleOrDefault(imageEntity => imageEntity.Id == image.Id);
                    if(referenceImageEntity != null)
                    {
                        offerToUpdate.Images.ToList().Add(new ImageAdress()
                        {
                            Value = image.Value 
                        });
                    }
                    else
                    {
                        if(image.Value != null)
                            referenceImageEntity.Value = image.Value;
                    }
                }                
            }
            if(offerUpdater.OfferTags != null && offerUpdater.OfferTags.Count() != 0)
            {
                foreach(var offerTagUpdater in offerUpdater.OfferTags)
                {
                    ///TODO do not allow for two offer tags of the same name
                    var referenceOfferEntity = offerToUpdate.OfferTags.SingleOrDefault(offerTagEntity => offerTagEntity.Name == offerTagUpdater.Name);
                    if(referenceOfferEntity != null)
                    {
                        offerToUpdate.OfferTags.ToList().Add(new OfferTag() { Name = offerTagUpdater.Name, Value = offerTagUpdater.Value });
                    }
                    else
                    {
                        if (offerTagUpdater.Name != null)
                            referenceOfferEntity.Name = offerTagUpdater.Name;
                        if(offerTagUpdater.Value != null)
                        {
                            referenceOfferEntity.Value = offerTagUpdater.Value;
                        }
                    }
                }
            }
            context.SaveChanges();
        }

        private IEnumerable<Offer> getOfferWithinSpecifiedDistance(LocationModel referenceLocation, double? maxDistance, List<Offer> offers)
        {
            var referencePoint = new Coordinate()
            {
                Latitude = referenceLocation.Lattitue,
                Longitude = referenceLocation.Longitude
            };

            return offers.Where(offer =>
            {
                var location = offer.Location;
                double distance = GeoCalculator.GetDistance(referencePoint, new Coordinate()
                {
                    Latitude = offer.Location.Lattitue,
                    Longitude = offer.Location.Longitude
                });

                return distance < maxDistance;
            });
        }
    }
}
