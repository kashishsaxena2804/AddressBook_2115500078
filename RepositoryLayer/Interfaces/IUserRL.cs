using ModelLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        User Register(User user);
        string Login(string email, string password);
        string GenerateResetToken(string email);
        bool ResetPassword(string email, string token, string newPassword);
        User GetUserByEmail(string email);
        void SaveResetToken(string email, string token);
        User GetUserByResetToken(string token);
    }
}
