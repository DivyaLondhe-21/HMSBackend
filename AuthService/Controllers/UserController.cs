using Microsoft.AspNetCore.Mvc;
using AuthService.Models;
using AuthService.JwtService;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { UserId = 1, Email = "divya21@example.com", Password = "password1", Role = "Admin" },
            new User { UserId = 2, Email = "londhe@example.com", Password = "password2", Role = "User" }
        };

        private readonly JwtToken _jwtToken;
        public UserController(JwtToken jwtToken)
        {
            _jwtToken = jwtToken;
        }

        /* [HttpGet]
         public ActionResult login()
         {
             return View;
         }*/
        // Registration (Create User)
        [HttpPost("register")]
        public ActionResult<User> RegisterUser(User user)
        {
            user.UserId = users.Max(u => u.UserId) + 1;
            users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // Login (Authenticate User)
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            Console.WriteLine($"Received login request: {loginRequest.Email}, {loginRequest.Password}");
            var user = users.FirstOrDefault(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            var token = _jwtToken.GenerateJwtToken(user.Email);
            // Return User info (or you could generate a JWT Token here)
            return Ok(new { message = "Login successful", username = user.Email, role = user.Role, token });
        }

        // Get all users (for testing, can be removed in production)
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(users);
        }

        // Get a specific user
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Update user info
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.Role = updatedUser.Role;
            return NoContent();
        }

        // Delete user
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            users.Remove(user);
            return NoContent();
        }
    }

    // Simple Login Request Model
    
}
