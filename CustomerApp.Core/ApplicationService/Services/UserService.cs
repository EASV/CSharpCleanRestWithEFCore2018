using System;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService.Services
{
    public class UserService: IUserService
    {
        readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        public FilteredList<User> GetAllUsers(Filter filter = null)
        {
            return _userRepo.ReadAll(filter);
        }


        public User CreateUser(User user, string readablePassword)
        {
            if (user == null)
                throw new ArgumentNullException(nameof (user));
            if (readablePassword == null)
                throw new ArgumentNullException(nameof (readablePassword));
            
            //Add minimum password length!!


            return _userRepo.CreateUser(user, readablePassword);
        }
        
        public User SignIn(User user, string readablePassword)
        {
            
            //Add minimum password length!!


            return _userRepo.SignIn(user, readablePassword);
        }

    }
}
