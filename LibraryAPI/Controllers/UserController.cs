using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICrudService<User, int> _userService; //dependency injection, if anyone requests for icrud service, give them to do service
        public UserController(ICrudService<User, int> userService)
        {
            _userService = userService;
        }

        // GET all action
        [HttpGet] // auto returns data with a Content-Type of application/json
        public ActionResult<List<User>> GetAll() => _userService.GetAll().ToList();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userService.Get(id);
            if (user is null) return NotFound();
            else return user;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(User user) //creating a new to do item
        {
            // Runs validation against model using data validation attributes
            if (ModelState.IsValid)
            {
                _userService.Add(user);
                return CreatedAtAction(nameof(Create), new { id = user.UserId }, user);
            }
            return BadRequest();
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            var existingUser = _userService.Get(id);
            if (existingUser is null || existingUser.UserId != id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _userService.Update(existingUser, user);
                return NoContent();
            }
            return BadRequest();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.Get(id);
            if (user is null) return NotFound();
            _userService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        [Route("info")]

        public ActionResult<List<string>> GetInfo(int id)
        {
            return ((UserService)_userService).GetJoinedData().ToList();
        }
    }
}

