using System;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Infrastructure.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        readonly CustomerAppContext _ctx;

        public UserRepository(CustomerAppContext ctx)
        {
            _ctx = ctx;
        }
        
        public FilteredList<User> ReadAll(Filter filter)
        {
            //Create a Filtered List
            var filteredList = new FilteredList<User>();
            
            //If there is a Filter then filter the list and set Count
            if (filter != null && filter.ItemsPrPage > 0 && filter.CurrentPage > 0)
            {
                filteredList.List = _ctx.Users
                    .Select(u => new User { Id = u.Id, Email = u.Email})
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage);
                filteredList.Count = _ctx.Users.Count();
                return filteredList;
            }
            
            //Else just return the full list and get the count from the list (to save a SQL call)
            filteredList.List = _ctx.Users
                .Select(u => new User { Id = u.Id, Email = u.Email});
            filteredList.Count = filteredList.List.Count();
            return filteredList;
        }

        public User CreateUser(User user, string readablePassword)
        {
            if (string.IsNullOrEmpty(user.UserName)) user.UserName = user.Email;
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, readablePassword);
            
            //Getting Role from DB, to also get the roles name for use in TokenManager later
            user.Role = _ctx.Roles.FirstOrDefault(r => r.Id == user.Role.Id);
            var savedUser = _ctx.Users.Add(user).Entity;
            _ctx.SaveChanges();
            return savedUser;
        }

        public User SignIn(User user, string readablePassword)
        {
            var userFromDB = string.IsNullOrEmpty(user.Email)
                ? _ctx.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.UserName.Equals(user.UserName))
                : _ctx.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Email.Equals(user.Email));
            if(userFromDB == null) throw new InvalidDataException("User Not Found");

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, userFromDB.PasswordHash, readablePassword);
            if(result == PasswordVerificationResult.Failed) throw new AuthenticationException("User failed to log in");
            return userFromDB;
        }
    }
}