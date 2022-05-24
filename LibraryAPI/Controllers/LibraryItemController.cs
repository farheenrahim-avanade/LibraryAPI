using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")] /*URL: http://localhost:5066/todo */
    public class LibraryItemController : ControllerBase
    {
        private readonly ICrudService<LibraryItem, int> _libraryitemService; //dependency injection, if anyone requests for icrud service, give them to do service
        public LibraryItemController(ICrudService<LibraryItem, int> libraryitemService)
        {
            _libraryitemService = libraryitemService;
        }

        // GET all action
        [HttpGet] // auto returns data with a Content-Type of application/json
        public ActionResult<List<LibraryItem>> GetAll() => _libraryitemService.GetAll().ToList();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<LibraryItem> Get(int id)
        {
            var libraryitem = _libraryitemService.Get(id);
            if (libraryitem is null) return NotFound();
            else return libraryitem;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(LibraryItem libraryitem) //creating a new to do item
        {
            // Runs validation against model using data validation attributes
            if (ModelState.IsValid)
            {
                _libraryitemService.Add(libraryitem);
                return CreatedAtAction(nameof(Create), new { id = libraryitem.LibraryItemId }, libraryitem);
            }
            return BadRequest();
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, LibraryItem libraryitem)
        {
            var existingLibraryItem = _libraryitemService.Get(id);
            if (existingLibraryItem is null || existingLibraryItem.LibraryItemId != id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _libraryitemService.Update(existingLibraryItem, libraryitem);
                return NoContent();
            }
            return BadRequest();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var libraryitem = _libraryitemService.Get(id);
            if (libraryitem is null) return NotFound();
            _libraryitemService.Delete(id);
            return NoContent();
        }
    }
}