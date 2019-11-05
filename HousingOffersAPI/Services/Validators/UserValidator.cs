using HousingOffersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Validators
{
    public class UserValidator : IUserValidator
    {
        private int lengthLimit = 25;
        public string IsUserValid(UserModel user)
        {
            //TODO validation for new user creation
            if (user.Email == null)
                return "invalid email!";

            if (user.Email.Count() > lengthLimit)
                return "email too long!";
            if (user.Login.Count() > lengthLimit)
                return "login too long!";
            if (user.Password.Count() > lengthLimit)
                return "password too long!";

            return null;
        }
    }
}
