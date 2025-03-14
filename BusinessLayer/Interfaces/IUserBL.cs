using ModelLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        User Register(User user);
        string Login(string email, string password);
        string GenerateResetToken(string email);
        bool ResetPassword(string email, string token, string newPassword);
    }
}
