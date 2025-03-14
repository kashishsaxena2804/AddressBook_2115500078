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
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Hash password before storing
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public string Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null; // Invalid login

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
    }
}
