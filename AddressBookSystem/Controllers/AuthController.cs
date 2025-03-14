using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ModelLayer.Models;

namespace AddressBookSystem.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserBL _userBL;

        public AuthController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var registeredUser = _userBL.Register(user);
            return registeredUser != null ? Ok(new { message = "User registered successfully" }) : BadRequest("Registration failed");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var token = _userBL.Login(loginRequest.Email, loginRequest.Password);
            return token != null ? Ok(new { message = "Login successful", token }) : Unauthorized("Invalid email or password");
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var token = _userBL.GenerateResetToken(model.Email);
            return token != null ? Ok("Password reset email sent!") : NotFound("User not found");
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto model)
        {
            var success = _userBL.ResetPassword(model.Email, model.Token, model.NewPassword);
            return success ? Ok("Password has been reset!") : BadRequest("Invalid token or expired");
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
