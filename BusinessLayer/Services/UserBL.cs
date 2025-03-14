using BusinessLayer.Interfaces;
using ModelLayer.Models;
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
    }
}
