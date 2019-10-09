using HousingOffersAPI.Models;

namespace HousingOffersAPI.Validators
{
    public interface IUserCreationValidator
    {
        bool isValid(UserModel user);
    }
}