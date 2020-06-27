using Crosscutting;
using System;
using System.Collections.Generic;
using User.Domain;
using UserDomain.Services;

namespace UserRESTFULapiTest
{
    public class UserServiceFake : IUserService
    {
        private readonly List<UserEntity> _userEntities;

        public UserServiceFake()
        {
            _userEntities = new List<UserEntity>()
            {
                new UserEntity(){CreatedAt = DateTime.Now, Email = "emerenciano.tiago@gmail.com", FirstName = "Tiago", LastName = "Emerenciano", Password = "12345", Phones = new List<PhoneEntity>() { new PhoneEntity() { AreaCode = 81, CountryCode = "+55", Number = 991722229, PhoneId = 1 } } },
                new UserEntity(){CreatedAt = DateTime.Now, Email = "hello@world.com", FirstName = "Hello", LastName = "World", Password = "hunter2", Phones = new List<PhoneEntity>() { new PhoneEntity() { AreaCode = 81, CountryCode = "+55", Number = 988887888, PhoneId = 1 } } }
            };
        }

        public Response LoggedUser(string email)
        {
            throw new System.NotImplementedException();
        }

        public UserEntity Signin(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public Response Signup(UserEntity user)
        {
            _userEntities.Add(user);
            return Response.CreateResponse(user);
        }
    }
}
