using System.Collections.Generic;
using System.Linq;
using AuthService.Models;
using global::AuthService.Models;
using AuthService.Interface;

    namespace AuthService.Repositories
    {
        public class UserRepository : IUser
        {
            private static List<User> users = new List<User>
        {
            new User { UserId = 1, Email = "divya21@example.com", Password = "password1", Role = "Admin" },
            new User { UserId = 2, Email = "londhe@example.com", Password = "password2", Role = "User" }
        };

            public IEnumerable<User> GetAllUsers()
            {
                return users;
            }

            public User GetUserById(int id)
            {
                return users.FirstOrDefault(u => u.UserId == id);
            }

            public void AddUser(User user)
            {
                user.UserId = users.Max(u => u.UserId) + 1;
                users.Add(user);
            }

            public void UpdateUser(User updatedUser)
            {
                var user = users.FirstOrDefault(u => u.UserId == updatedUser.UserId);
                if (user != null)
                {
                    user.Email = updatedUser.Email;
                    user.Password = updatedUser.Password;
                    user.Role = updatedUser.Role;
                }
            }

            public void DeleteUser(int id)
            {
                var user = users.FirstOrDefault(u => u.UserId == id);
                if (user != null)
                {
                    users.Remove(user);
                }
            }

            public User Authenticate(string email, string password)
            {
                return users.FirstOrDefault(u => u.Email == email && u.Password == password);
            }
        }
    }


