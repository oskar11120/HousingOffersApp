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

        public int? GetUserID(UserModel userModel)
        {
            var neededUser = context.Users.SingleOrDefault(user =>
            (user.Login == userModel.Login || user.Email == userModel.Email)
            && user.Password == userModel.Password);

            if (neededUser == null)
                return null;
            else
                return neededUser.Id;
        }

        public void AddUser(UserModel user)
        {           
            context.Users.Add(AutoMapper.Mapper.Map<Models.UserModel, Entities.User>(user));
            context.SaveChanges();
        }
    }
}
