using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

namespace AddressBookSystem.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;

        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var registeredUser = _userBL.Register(user);
            if (registeredUser == null)
                return BadRequest(new { message = "Registration failed" });

            return Ok(new { message = "User registered successfully", user = registeredUser });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var token = _userBL.Login(loginRequest.Email, loginRequest.Password);
            if (token == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new { message = "Login successful", token });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
