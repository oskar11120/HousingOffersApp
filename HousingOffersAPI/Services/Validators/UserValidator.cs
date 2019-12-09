using HousingOffersAPI.Models;
using HousingOffersAPI.Services.UsersRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Validators
{
    public class UserValidator : IUserValidator
    {
        private int lengthLimit = 25;
        public UserValidator(IUsersRepozitory usersRepozitory)
        {
            this.usersRepozitory = usersRepozitory;
        }

        private readonly IUsersRepozitory usersRepozitory;

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
            if (usersRepozitory.DoesUserWithLoginExist(user.Login))
                return "login taken by another user!";
            if (usersRepozitory.DoesUserWithMailExist(user.Email))
                return "there is an account tied to given email!";
            return null;
        }
    }
}
