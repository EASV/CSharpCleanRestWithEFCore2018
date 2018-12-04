using CustomerApp.Core.Entity;

namespace CustomerApp.Core.DomainService
{
    public interface IUserRepository
    {
        FilteredList<User> ReadAll(Filter filter);
        User CreateUser(User user, string readablePassword);
        User SignIn(User user, string readablePassword);
    }
   
}