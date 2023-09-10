using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Data;
using ToDoListApi.Dtos;

//https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ToDoItem _toDoItem;

        public ToDoItemsController(ApplicationDbContext db, ToDoItem toDoItem)
        {
            _db = db;
            _toDoItem = toDoItem;
        }

        [HttpGet]
        public List<ToDoItem> GetToDoItems()
        {
            return _db.ToDoItems.OrderBy(t => t.Done).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoItem> GetToDoItem(int id)
        {
            var toDoItem = _db.ToDoItems.Find(id);

            if (toDoItem == null)
                return NotFound();

            return toDoItem;
        }

        [HttpPost]
        public ActionResult<ToDoItem> PostToDoItem(PostToDoItem postToDoItem)
        {
            if (ModelState.IsValid)
            {
                _toDoItem.Title = postToDoItem.Title;
                _toDoItem.Done = postToDoItem.Done;

                _db.Add(_toDoItem);
                _db.SaveChanges();
                return CreatedAtAction(nameof(GetToDoItem), new { id = _toDoItem.Id }, _toDoItem);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDoItem(int id)
        {
            var toDoItem = _db.ToDoItems.Find(id);

            if (toDoItem == null)
                return NotFound();

            _db.Remove(toDoItem);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutToDoItem(int id, PutToDoItem putToDoItem)
        {
            var toDoItem = _db.ToDoItems.Find(id);

            if (toDoItem == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                toDoItem.Title = putToDoItem.Title;
                toDoItem.Done = putToDoItem.Done;

                _db.SaveChanges();
                return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}
