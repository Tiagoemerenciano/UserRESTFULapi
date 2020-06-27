using User.Domain;

namespace UserDomain.Repositories
{
    public interface IUserRepository
    {
        void InsertUser(UserEntity user);
        void UpdateUser(UserEntity user);
        UserEntity SelectUser(string email);
    }
}
