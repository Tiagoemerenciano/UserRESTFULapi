using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using User.Domain;
using UserDomain.Services;
using UserRESTFULapi.Controllers;
using Xunit;

namespace UserRESTFULapiTest
{
    public class UserControllerTest
    {
        UserController _controller;
        IUserService _service;

        public UserControllerTest()
        {
            _service = new UserServiceFake();
            _controller = new UserController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            IActionResult okResult = _controller.Signup(new UserEntity() { CreatedAt = DateTime.Now, Email = "emerenciano.tiago@gmail.com", FirstName = "Tiago", LastName = "Emerenciano", Password = "12345", Phones = new List<PhoneEntity>() { new PhoneEntity() { AreaCode = 81, CountryCode = "+55", Number = 991722229, PhoneId = 1 } } });
        }
    }
}
