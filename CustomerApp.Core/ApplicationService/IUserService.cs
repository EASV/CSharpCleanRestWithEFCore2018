using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService
{
    public interface IUserService
    {
        FilteredList<User> GetAllUsers(Filter filter);
        
        User CreateUser(User user, string readablePassword);

        User SignIn(User user, string readablePassword);
    }
}