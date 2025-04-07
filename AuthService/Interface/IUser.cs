using System.Collections.Generic;
using AuthService.Models;

namespace AuthService.Interface
{
    public interface IUser
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        User Authenticate(string email, string password);
    }
}
