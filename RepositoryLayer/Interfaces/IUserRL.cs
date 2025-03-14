using ModelLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        User Register(User user);
        string Login(string email, string password);
    }
}
