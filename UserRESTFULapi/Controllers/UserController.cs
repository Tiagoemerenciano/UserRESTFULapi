using Crosscutting;
using Microsoft.AspNetCore.Mvc;
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
            if (CheckFieldsError())
            {
                return BadRequest(ErrorResponse.CreateErrorResponse("Invalid fields", 3));
            }

            if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.FirstName) ||
                string.IsNullOrWhiteSpace(user.LastName) || string.IsNullOrWhiteSpace(user.Password) || user.Phones == null ||
                user.Phones.Any(p => p.AreaCode == 0 || p.CountryCode == null || p.Number == 0) || user.Phones.Count == 0)
            {
                return BadRequest(ErrorResponse.CreateErrorResponse("Missing fields", 4));
            }
            else if (!new EmailAddressAttribute().IsValid(user.Email))
            {
                return BadRequest(ErrorResponse.CreateErrorResponse("Invalid fields", 3));
            }
            else
            {
                return Ok(_userService.Signup(user));
            }
        }

        [HttpPost("signin")]
        public IActionResult Signin([FromBody] CredentialsModel credentials)
        {
            if (CheckFieldsError())
            {
                return BadRequest(ErrorResponse.CreateErrorResponse("Invalid fields", 3));
            }

            if (credentials == null || string.IsNullOrWhiteSpace(credentials.Email) || string.IsNullOrWhiteSpace(credentials.Password))
            {
                return BadRequest(ErrorResponse.CreateErrorResponse("Missing fields", 4));
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
                return Ok(ErrorResponse.CreateErrorResponse("Invalid e-mail or password", 5));
            }
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            string userEmail = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(ErrorResponse.CreateErrorResponse("Authorization header required", 7));
            }

            if (userEmail == null)
            {
                return Unauthorized(ErrorResponse.CreateErrorResponse("Unauthorized", 6));
            }

            dynamic loggedUser = _userService.LoggedUser(userEmail);

            if (loggedUser.GetType().Name == "ErrorResponse")
            {
                return Unauthorized(loggedUser);
            }
            else
            {
                return Ok(loggedUser);
            }
        }

        private bool CheckFieldsError()
        {
            foreach (var modelError in ModelState)
            {
                if (modelError.Value.Errors.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
