using HousingOffersAPI.Models;

namespace HousingOffersAPI.Services.UsersRelated
{
    public interface IUsersRepozitory
    {
        int? GetUserID(UserModel userModel);
        void AddUser(UserModel user);
        void DeleteUser(int userId);
        void UpdateUser(UserModel user);
    }
}