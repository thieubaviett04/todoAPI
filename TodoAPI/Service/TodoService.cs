using TodoAPI.Data;
using TodoAPI.DTOs;
using TodoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoAPI.Service
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;
        public TodoService(TodoDbContext context) { 
        
            _context = context;

        }

        public async Task<TodoItemDto> CreateAsync(CreateItemDto createItemDto)
        {
            var item = new TodoItem
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                IsComplete = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            var result = new TodoItemDto
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete,
                Description = item.Description,
                CreatedAt = item.CreatedAt
            };

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            
            if (item == null)
            {
                return false;
            }

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<List<TodoItemDto>> GetAllAsync()
        {
            var items = _context.TodoItems
                .Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsComplete = item.IsComplete,
                    Description = item.Description,
                    CreatedAt = item.CreatedAt
                })
                .ToListAsync();

            return items;
        }

        public Task<TodoItemDto?> GetByIdAsync(int id)
        {
            var item = _context.TodoItems.Where(item => item.Id == id)
                .Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsComplete = item.IsComplete,
                    Description = item.Description,
                    CreatedAt = item.CreatedAt
                })
                .FirstOrDefaultAsync();

            return item;
        }

        public async Task<bool> PathAsync(int id)
        {
            var item = _context.TodoItems.Find(id);

            if(item == null)
            {
                return false;
            }

            item.IsComplete = !item.IsComplete;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(int id, UpdateItemDto updateItemDto)
        {
            var item = await _context.TodoItems.FindAsync(id);
            
            if(item == null)
            {
                return false;
            }

            item.Name = updateItemDto.Name;
            item.Description = updateItemDto.Description;
            item.IsComplete = updateItemDto.IsComplete;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
