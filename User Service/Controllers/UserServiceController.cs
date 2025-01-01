// Example: UserServiceController.cs
using Microsoft.AspNetCore.Mvc;
using ECommerce.DataService.UserData;
using Microsoft.AspNetCore.Authorization;
using ECommerce.DataService.UserModels.Users;
using Microsoft.VisualBasic.ApplicationServices;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        private readonly DbContext_User _context;

        public UserServiceController(DbContext_User context)
        {
            _context = context;
        }
        [Authorize]
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(ECommerce.DataService.UserModels.Users.User user  )
        {

          var _user =  _context.Users.Add(user);
            return Ok(_user);
        }
        [HttpGet("GetUserByID/{id}")]
        public IActionResult GetUserByID(int id)
        {
            var users = _context.Users.Where(user => user.UserId == id);
            return Ok(users);
        }

        [HttpPost("UpdateByID")]
        public IActionResult UpdateByID(ECommerce.DataService.UserModels.Users.User user)
        {


            var _user = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }
            _user.Name = user.Name;
            _user.Email = user.Email;
            _user.Phone = user.Phone;
            _user.DateOfBirth = user.DateOfBirth;
            _user.Age = user.Age;
            _user.ModifiedBy = user.ModifiedBy;
            _user.ModifiedDate = user.ModifiedDate;
            _user.Status = user.Status;
            _context.SaveChanges();



            return Ok(_user);
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers() {


            var users = _context.Users.ToList();
            return Ok(users);
        }
    }
}
