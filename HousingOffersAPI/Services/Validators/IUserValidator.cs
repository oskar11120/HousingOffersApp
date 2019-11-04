using HousingOffersAPI.Models;

namespace HousingOffersAPI.Validators
{
    public interface IUserValidator
    {
        //returns error message if error appears and null if there is no error
        string IsUserValid(UserModel user);
    }
}