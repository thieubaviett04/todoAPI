using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;
using TodoAPI.Data;
using TodoAPI.DTOs;

namespace TodoAPI.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoDbContext _context;

        public TodosController(TodoDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.TodoItems
                .Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    IsComplete = item.IsComplete,
                    CreatedAt = item.CreatedAt
                })
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.TodoItems.Where(item => item.Id == id)
                .Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    IsComplete = item.IsComplete,
                    CreatedAt = item.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

             return Ok(item);
        }

        [HttpPost] 
        public async Task<IActionResult> CreateItem(CreateItemDto itemDto)
        {
            var item = new TodoItem
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                IsComplete = false,
                CreatedAt = DateTime.UtcNow
            };
            
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            var createdItem = new TodoItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                IsComplete = item.IsComplete,
                CreatedAt = item.CreatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, createdItem);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, UpdateItemDto updateItemDto)
        {
            var item = await _context.TodoItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.Name = updateItemDto.Name;
            item.Description = updateItemDto.Description;
            item.IsComplete = updateItemDto.IsComplete;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> MarkAsComplete(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            item.IsComplete = true;
            await _context.SaveChangesAsync();

            var itemDto = new TodoItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                IsComplete = item.IsComplete,
                CreatedAt = item.CreatedAt
            };

            return Ok(itemDto);
        }
    }
}
