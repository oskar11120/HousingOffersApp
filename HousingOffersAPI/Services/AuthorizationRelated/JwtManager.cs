﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using HousingOffersAPI.Options;

namespace HousingOffersAPI.Services.Validators
{
    public class JwtManager : IJwtManager
    {
        public JwtManager(IOffersRepozitory repozitory, IOptions<ApiOptions> options)
        {
            this.repozitory = repozitory;
            this.securityKey = options.Value.UsersControllerOptions.SecurityKeys["JWT"];
        }

        private readonly IOffersRepozitory repozitory;
        private readonly string securityKey;

        public bool IsClaimValidToRequestedUserId(int requestedUserId, Claim[] claims)
        {
            return GetClaimOfType(claims, "UserId") == requestedUserId;
        }
        public int GetClaimOfType(Claim[] claims, string type)
        {
           return int.Parse(claims.Single(claim => claim.Type == type).Value);
        }

        public JwtSecurityToken CreateJWT(int userId)
        {
            var claims = new[]
                {
                    new Claim("UserId", userId.ToString())
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return token;
        }

        public bool IsClaimValidToRequestedOfferId(int requestedOfferId, Claim[] claims)
        {
            var listWithNeededOffer = repozitory.GetOffers(new Models.OffersRequestContentModel()
            {
                OfferId = requestedOfferId
            });
            if (listWithNeededOffer.Count() == 0) return false;

            return IsClaimValidToRequestedUserId(listWithNeededOffer.First().UserId, claims);
        }
    }
}
