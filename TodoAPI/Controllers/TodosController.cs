using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private static List<TodoItem> items = new() 
        { 
            
            new TodoItem
            {
                Id = 1,
                Name = "Hoc ASP .NET core bai so 22",
                IsComplete = false,
                Description = "Hoc lam quen voi web API.",
               
            },

            new TodoItem
            {
                Id = 2,
                Name = "Hoc ASP .NET core bai so 23",
                IsComplete = false,
                Description = "Cai dat cau hinh cho authentication cho TodoList.",

            }

        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost] 
        public IActionResult CreateItem(TodoItem item)
        {
            var newId = items.Max(x => x.Id) + 1;
            item.Id = newId;
            item.CreatedAt = DateTime.UtcNow;
            item.IsComplete = false;

            items.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id}, item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, TodoItem updatedItem)
        {
            var item = items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description;
            item.IsComplete = updatedItem.IsComplete;
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var item = items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            items.Remove(item);
            return NoContent();
        }

        [HttpPatch("{id}/complete")]
        public IActionResult MarkAsComplete(int id)
        {
            var item = items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            item.IsComplete = true;
            return Ok(item);
        }




    }
}
