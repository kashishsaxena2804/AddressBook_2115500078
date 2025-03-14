using ModelLayer.Models;
using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRL;

        public UserBL(IUserRL userRL)
        {
            _userRL = userRL;
        }

        public User Register(User user)
        {
            return _userRL.Register(user);
        }

        public string Login(string email, string password)
        {
            return _userRL.Login(email, password);
        }

        public string GenerateResetToken(string email)
        {
            return _userRL.GenerateResetToken(email);
        }

        public bool ResetPassword(string email, string token, string newPassword)
        {
            return _userRL.ResetPassword(email, token, newPassword);
        }
    }
}
