using HousingOffersAPI.Models;

namespace HousingOffersAPI.Services.UsersRelated
{
    public interface IUsersRepozitory
    {
        bool DoesUserExist(UserModel userModel);
    }
}