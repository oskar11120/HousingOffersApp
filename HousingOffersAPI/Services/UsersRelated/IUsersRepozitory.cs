using HousingOffersAPI.Entities;
using HousingOffersAPI.Models;

namespace HousingOffersAPI.Services.UsersRelated
{
    public interface IUsersRepozitory
    {
        int? GetUserID(UserModel userModel);
        User GetUser(int userId);
        void AddUser(UserModel user);
        void DeleteUser(int userId);
        void UpdateUser(UserModel user, int userId);
    }
}