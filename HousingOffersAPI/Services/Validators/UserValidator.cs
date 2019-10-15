using HousingOffersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Validators
{
    public class UserValidator : IUserValidator
    {
        public bool IsNewUserValid(UserModel user)
        {
            //TODO validation for new user creation
            return true;
        }
    }
}
