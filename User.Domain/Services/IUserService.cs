using Crosscutting;
using User.Domain;

namespace UserDomain.Services
{
    public interface IUserService
    {
        Response Signup(UserEntity user);
        UserEntity Signin(string email, string password);
        Response LoggedUser(string email);
    }
}
