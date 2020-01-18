﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HousingOffersAPI.Entities;
using HousingOffersAPI.Models;
using HousingOffersAPI.Models.AnalyticsRelated;
using HousingOffersAPI.Models.DatabaseRelated;
using HousingOffersAPI.Services;
using HousingOffersAPI.Services.ClicksRelated;
using HousingOffersAPI.Services.UsersRelated;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HousingOffersAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        public DummyController(IOffersRepozitory offersRepozitory, IUsersRepozitory usersRepozitory, IClicksRepository clicksRepository)
        {
            this.usersRepozitory = usersRepozitory;
            this.offersRepozitory = offersRepozitory;
            this.clicksRepository = clicksRepository;
        }

        private readonly IOffersRepozitory offersRepozitory;
        private readonly IUsersRepozitory usersRepozitory;
        private readonly IClicksRepository clicksRepository;

        [HttpGet("initdatabase")]
        public IActionResult InitializeDatabase()
        {
            if (offersRepozitory.GetOffers(new OffersRequestContentModel()).ToList().Count() != 0)
                return BadRequest("database already initialized");

            List<UserModel> userModels = new List<UserModel>()
            {
                new UserModel()
                {
                    Email = "aaaa@bbbb.cccc",
                    Login = "redBerry",
                    Password = "12345",
                    PhoneNumber = "123456789"
                },
                new UserModel()
                {
                    Email = "qwerty@asd.com",
                    Login = "qwe",
                    Password = "rty",
                    PhoneNumber = "125555759"
                },
                new UserModel()
                {
                    Email = "nologin@kkk.com",
                    Login = "Petro",
                    Password = "senko",
                    PhoneNumber = "12345611"
                }
            };
            userModels.ForEach(user => usersRepozitory.AddUser(user));


            List<int> userIds = userModels.Select(user => (int)usersRepozitory.GetUserID(user)).ToList();
            var offerModels = new List<OfferModel>()
            {
                new OfferModel()
                {
                    UserId = userIds[0],
                    Location = new LocationModel()
                    {
                        Lattitue = 5,
                        Longitude = 10,
                        Description = "budryka 5 budapeszt"
                    },
                    Area = 50,
                    Description = "asdasdasdasdasd",
                    Images = new List<ImageAdressModel>()
                    {
                        new ImageAdressModel()
                        {
                            Value = "image for first user"
                        },
                        new ImageAdressModel()
                        {
                            Value = "image for firt user #2"
                        }
                    },
                    OfferTags = new List<OfferTagModel>()
                    {
                        new OfferTagModel()
                        {
                            Name = "idisposable",
                            Value = "false"
                        },
                        new OfferTagModel()
                        {
                            Name = "icollection",
                            Value = "true"
                        }
                    },
                    OfferType = "wynajem",
                    PriceInPLN = 10000,
                    PropertyType = "mieszkanie"
                },
                new OfferModel()
                {
                    UserId = userIds[1],
                    Location = new LocationModel()
                    {
                        Lattitue = 7,
                        Longitude = 10,
                        Description = "adress for user 2"
                    },
                    Area = 100.5,
                    Description = "desc for user 2",
                    Images = new List<ImageAdressModel>()
                    {
                        new ImageAdressModel()
                        {
                            Value = "image for second user"
                        },
                        new ImageAdressModel()
                        {
                            Value = "image for second user #2"
                        }
                    },
                    OfferTags = new List<OfferTagModel>()
                    {
                        new OfferTagModel()
                        {
                            Name = "user2",
                            Value = "true"
                        },
                        new OfferTagModel()
                        {
                            Name = "tagforuser2",
                            Value = "true"
                        }
                    },
                    OfferType = "sprzedarz",
                    PriceInPLN = 99999999.99,
                    PropertyType = "działka zabudowana"
                },
                new OfferModel()
                {
                    UserId = userIds[2],
                    Location = new LocationModel()
                    {
                        Lattitue = 5,
                        Longitude = 5,
                        Description = "adress for user 3"
                    },
                    Area = 10,
                    Description = "zxzzzxczxczxc",
                    Images = new List<ImageAdressModel>()
                    {
                        new ImageAdressModel()
                        {
                            Value = "image for third user"
                        },
                        new ImageAdressModel()
                        {
                            Value = "image for third user nr 2"
                        }
                    },
                    OfferTags = new List<OfferTagModel>()
                    {
                        new OfferTagModel()
                        {
                            Name = "tag user 3 ",
                            Value = "3"
                        },
                        new OfferTagModel()
                        {
                            Name = "tag user 3 2",
                            Value = "3 2"
                        }
                    },
                    OfferType = "wynajem",
                    PriceInPLN = 100,
                    PropertyType = "działka"
                }
            };
            offerModels.ForEach(offer => offersRepozitory.AddOffer(offer));

            List<int> offerIds = offersRepozitory.GetOffers(new OffersRequestContentModel()).Select(offer => offer.Id)
                .ToList();
            var rng = new Random(1000);


            for (int i = 0; i < 2; i++)
            {
                offerIds.ForEach(offerId => clicksRepository.AddOfferClick(offerId));
                userIds.ForEach(userId => clicksRepository.AddUserClick(userId));
            }

            return Ok("database reset");
        }

        [HttpGet("resetdatabase")]
        public IActionResult ResetDatabase()
        {
            var offers = offersRepozitory.GetOffers(new OffersRequestContentModel())
                .ToList();
            var offerIds = offers.Select(offer => offer.Id)
                .ToList();
            var userIds = offers.Select(offer => offer.UserId)
                .ToList()
                .GroupBy(userId => userId)
                .Where(group => group.Count() == 1)
                .Select(group => group.Key)
                .ToList();

            offerIds.ForEach(offerId => offersRepozitory.DeleteOffer(offerId));
            userIds.ForEach(userId => usersRepozitory.DeleteUser(userId));

            return InitializeDatabase();
        }

    }
}