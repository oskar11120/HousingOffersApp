﻿using HousingOffersAPI.Entities;
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
            (user.Login == userModel.Login || user.Email == userModel.Email || user.PhoneNumber == userModel.PhoneNumber)
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
        public User GetUser(int userId)
        {
            var neededUser = context.Users.SingleOrDefault(user => user.Id == userId);

            var outUser = neededUser == null ? null : new User()
            {
                Email = neededUser.Email,
                Id = neededUser.Id,
                Login = neededUser.Login,
                Offers = neededUser.Offers,
                PhoneNumber = neededUser.PhoneNumber
            };
            return outUser;
        }
        public void DeleteUser(int userId)
        {
            var userToDelete = context.Users.SingleOrDefault(user => user.Id == userId);
            if(userToDelete != null)
            {
                context.Users.Remove(userToDelete);
                context.SaveChanges();
            }
        }
        public void UpdateUser(UserModel user, int userId)
        {
            var userToUpdate = context.Users.SingleOrDefault(userEntity => userEntity.Id == userId);

            if (userToUpdate == null) return;

            if (user.Login != null) userToUpdate.Login = user.Login;
            if (user.Password != null) userToUpdate.Password = user.Password;
            if (user.PhoneNumber != null) userToUpdate.PhoneNumber = user.PhoneNumber;            

            context.SaveChanges();
        }

        public bool DoesUserWithMailExist(string email)
        {
            return context.Users.Any(user => user.Email == email);
        }
        public bool DoesUserWithLoginExist(string login)
        {
            return context.Users.Any(user => user.Login == login);
        }

    }
}
