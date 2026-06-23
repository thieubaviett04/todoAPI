
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DTOs;
using TodoAPI.Service;

namespace TodoAPI.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TodoQueryDto todoQueryDto)
        {
            var items = await _todoService.GetAllAsync(todoQueryDto);
    
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _todoService.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

             return Ok(item);
        }

        [HttpPost] 
        public async Task<IActionResult> CreateItem(CreateItemDto itemDto)
        {
            var item = await _todoService.CreateAsync(itemDto);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, UpdateItemDto updateItemDto)
        {
            var item = await _todoService.UpdateAsync(id, updateItemDto);

            if (!item)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _todoService.DeleteAsync(id);

            return NoContent();
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> MarkAsComplete(int id)
        {
            var item = await _todoService.PathAsync(id);

            if (!item)
            {
                return NotFound();
            }

            return NoContent();

        }
    }
}
