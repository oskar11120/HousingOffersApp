using HousingOffersAPI.Entities;
using HousingOffersAPI.Models.AnalyticsRelated;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HousingOffersAPI.Services.ClicksRelated
{
    public class ClicksRepository : IClicksRepository
    {
        public ClicksRepository(HousingOffersContext context)
        {
            this.context = context;
        }

        private HousingOffersContext context;

        public void AddOfferClick(int offerId)
        {
            context.OfferClicks.Add(new OfferClick()
            {
                DateTime = DateTime.Now,
                OfferId = offerId
            });

            context.SaveChanges();
        }

        public List<OfferClickModel> GetOfferClicks()
        {
            return AutoMapper.Mapper.Map<List<OfferClick>, List<OfferClickModel>>(context.OfferClicks.ToList());
        }
      

        public void AddUserClick(int userId)
        {
            context.UserClicks.Add(new UserClick()
            {
                DateTime = DateTime.Now,
                UserId = userId
            });

            context.SaveChanges();
        }

        public List<UserClickModel> GetUserClicks()
        {
            return AutoMapper.Mapper.Map<List<UserClick>, List<UserClickModel>>(context.UserClicks.ToList());
        }
    }
}
