using Crosscutting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using User.Domain;
using UserDomain.Services;
using UserRESTFULapi.Dto;
using UserRESTFULapi.Models;

namespace UserRESTFULapi.Controllers
{
    [Route("/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public IActionResult Signup([FromBody] UserEntity user)
        {
            foreach(var modelError in ModelState)
            {
                if (modelError.Value.Errors.Count > 0)
                {
                    return BadRequest();
                }
            }

            if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName) || string.IsNullOrWhiteSpace(user.Password) || user.Phones.Any(p => p.AreaCode == 0 || p.CountryCode == null || p.Number == 0) || user.Phones.Count == 0)
            {
                return BadRequest(Crosscutting.Response.CreateErrorResponse("Missing fields", 4));
            }
            else if (!new EmailAddressAttribute().IsValid(user.Email))
            {
                return BadRequest(Crosscutting.Response.CreateErrorResponse("Invalid fields", 3));
            }
            else
            {
                user.CreatedAt = DateTime.Now;
                Crosscutting.Response result = _userService.Signup(user);
                return Ok(result);
            }
        }

        [HttpPost("signin")]
        public IActionResult Signin([FromBody] CredentialsModel credentials)
        {
            if (string.IsNullOrWhiteSpace(credentials.Email) || string.IsNullOrWhiteSpace(credentials.Password))
            {
                return BadRequest(Crosscutting.Response.CreateErrorResponse("Missing fields", 4));
            }

            UserEntity user = _userService.Signin(credentials.Email, credentials.Password);

            if (user != null)
            {
                SigninDto dto = new SigninDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    Phones = user.Phones
                };
                return Ok(Crosscutting.Response.CreateResponse(dto));
            }
            else
            {
                return Ok(Crosscutting.Response.CreateErrorResponse("Invalid e-mail or password", 5));
            }
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            string userEmail = Request.Headers["Authorization"].ToString();

            if (userEmail == null)
            {
                return Unauthorized(Crosscutting.Response.CreateErrorResponse("Unauthorized",6));
            }

            Crosscutting.Response loggedUser = _userService.LoggedUser(userEmail);

            return Unauthorized(loggedUser);
        }
    }
}
