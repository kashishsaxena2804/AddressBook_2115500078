using ModelLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        User Register(User user);
        string Login(string email, string password);
    }
}
