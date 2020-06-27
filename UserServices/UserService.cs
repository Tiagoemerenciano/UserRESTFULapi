using Crosscutting;
using System;
using User.Domain;
using UserDomain.Repositories;
using UserDomain.Services;

namespace UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Response Signup(UserEntity user)
        {

            UserEntity emailRegistered = _userRepository.SelectUser(user.Email);

            if (emailRegistered != null)
            {
                return Response.CreateErrorResponse("E-mail already exists", 1);
            }

            _userRepository.InsertUser(user);

            return Response.CreateResponse("Success");
        }

        public UserEntity Signin(string email, string password)
        {
            UserEntity user = _userRepository.SelectUser(email);

            if (user != null && user.Password.Equals(password))
            {
                user.LastLogin = DateTime.Now;

                _userRepository.UpdateUser(user);

                return user;
            }
            else
            {
                return null;
            }
        }

        public Response LoggedUser(string email)
        {
            UserEntity user = _userRepository.SelectUser(email);

            if (user.LastLogin.AddMinutes(5) > DateTime.Now)
            {
                return Response.CreateResponse(user);
            }
            else
            {
                return Response.CreateErrorResponse("Unauthorized - invalid session", 2);
            }

        }
    }
}
