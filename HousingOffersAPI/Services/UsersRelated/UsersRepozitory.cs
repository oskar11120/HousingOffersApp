using HousingOffersAPI.Entities;
using HousingOffersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Services.UsersRelated
{
    public class UsersRepozitory : IUsersRepozitory
    {
        public UsersRepozitory(HousingOffersContext context)
        {
            this.context = context;
        }

        private HousingOffersContext context;

        public bool DoesUserExist(UserModel userModel)
        {
            return context.Users.Any(user => 
            (user.Login == userModel.Login || user.Email == userModel.Email) 
            && user.Password == userModel.Password);
        }
    }
}
