using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using User.Domain;
using UserDomain.Repositories;
using UserRepositories;

namespace UserServices.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository;
        private UserService _userService;

        private readonly List<UserEntity> _userEntities;

        public UserServiceTests()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddTransient<IUserRepository, UserRepository>();
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _userRepository = serviceProvider.GetService<IUserRepository>();

            _userService = new UserService(_userRepository);

            _userEntities = new List<UserEntity>() { new UserEntity() { Email = "hello@world.com", FirstName = "Hello", LastName = "World", Password = "hunter2", Phones = new List<PhoneEntity>() { new PhoneEntity() { AreaCode = 81, CountryCode = "+55", Number = 988887888 } } } };
        }

        [TestMethod()]
        public void SignupTest()
        {
            UserEntity user = new UserEntity() { Email = "hello@world.com", FirstName = "Hello", LastName = "World", Password = "hunter2", Phones = new List<PhoneEntity>() { new PhoneEntity() { AreaCode = 81, CountryCode = "+55", Number = 988887888 } } };

            string expected = "Success";

            string result = _userService.Signup(user).Message;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SigninTest()
        {
            var result = _userService.Signin("hello@world.com", "hunter2");
            Assert.AreEqual(null, result);
        }
    }
}