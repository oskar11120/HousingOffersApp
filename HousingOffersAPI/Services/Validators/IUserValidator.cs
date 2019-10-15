using HousingOffersAPI.Models;

namespace HousingOffersAPI.Validators
{
    public interface IUserValidator
    {
        bool IsNewUserValid(UserModel user);
    }
}