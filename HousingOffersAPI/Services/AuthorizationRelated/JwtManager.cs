using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HousingOffersAPI.Services.Validators
{
    public class JwtManager : IJwtManager
    {
        public JwtManager(IOffersRepozitory repozitory, Microsoft.Extensions.Options.IOptions<List<string>> options)
        {
            this.repozitory = repozitory;
            this.securityKey = options.Value[0];
        }

        private readonly IOffersRepozitory repozitory;
        private readonly string securityKey;

        public bool IsClaimValidToRequestedUserId(int requestedUserId, Claim[] claims)
        {
            return getClaimedUserId(claims) == requestedUserId;
        }
        public int getClaimedUserId(Claim[] claims)
        {
           return int.Parse(claims.Single(claim => claim.Type == "UserId").Value);
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

            return IsClaimValidToRequestedUserId(listWithNeededOffer.ToList()[0].UserId, claims);
        }
    }
}
