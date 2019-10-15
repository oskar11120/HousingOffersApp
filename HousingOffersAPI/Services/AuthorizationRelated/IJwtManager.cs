using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HousingOffersAPI.Services.Validators
{
    public interface IJwtManager
    {
        bool IsClaimValidToRequestedUserId(int requestedUserId, Claim[] claims);
        bool IsClaimValidToRequestedOfferId(int requestedOfferId, Claim[] claims);
        int getClaimedUserId(Claim[] claims);
        JwtSecurityToken CreateJWT(int userId);
    }
}