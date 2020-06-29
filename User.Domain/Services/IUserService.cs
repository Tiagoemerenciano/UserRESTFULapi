using User.Domain;

namespace UserDomain.Services
{
    public interface IUserService
    {
        dynamic Signup(UserEntity user);
        UserEntity Signin(string email, string password);
        dynamic LoggedUser(string email);
    }
}
