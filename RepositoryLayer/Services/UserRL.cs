using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Context;
using System.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly AddressBookDbContext _context;
        private readonly IConfiguration _configuration;

        public UserRL(AddressBookDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public User Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public string Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void SaveResetToken(string email, string token)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                user.ResetToken = token;
                user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(30);
                _context.SaveChanges();
            }
        }

        public User GetUserByResetToken(string token)
        {
            return _context.Users.FirstOrDefault(u => u.ResetToken == token && u.ResetTokenExpiry > DateTime.UtcNow);
        }

        public string GenerateResetToken(string email)
        {
            var user = GetUserByEmail(email);
            if (user == null) return null;

            string token = Guid.NewGuid().ToString();
            SaveResetToken(email, token);

            SendResetEmail(user.Email, token);
            return token;
        }

        private void SendResetEmail(string toEmail, string token)
        {
            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_configuration["EmailSettings:Port"]),
                Credentials = new NetworkCredential(
                    _configuration["EmailSettings:SenderEmail"],
                    _configuration["EmailSettings:SenderPassword"]),
                EnableSsl = true
            };


            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:SenderEmail"]),
                Subject = "Password Reset",
                Body = $"Use this token to reset your password: {token}",
                IsBodyHtml = false,
            };

            mailMessage.To.Add(toEmail);
            smtpClient.Send(mailMessage);
        }

        public bool ResetPassword(string email, string token, string newPassword)
        {
            var user = GetUserByResetToken(token);
            if (user == null) return false;

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.ResetToken = "";
            user.ResetTokenExpiry = null;
            _context.SaveChanges();

            return true;
        }
    }
}
