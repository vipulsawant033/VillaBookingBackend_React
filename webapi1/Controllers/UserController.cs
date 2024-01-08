using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using webapi1.Models;

namespace webapi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly VillaContext _context;

        public UserController(VillaContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Username and password are required");
            }


            var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists");
            }


            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);


            user.Password = hashedPassword;


            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);

            if (existingUser == null)
            {
                return Unauthorized("Invalid credentials");
            }


            if (!BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password))
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok("Login successful");
        }
    }
}