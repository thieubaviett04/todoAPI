using TodoAPI.DTOs;

namespace TodoAPI.Service
{
    public interface ITodoService
    {
        Task<List<TodoItemDto>> GetAllAsync();
        Task<TodoItemDto?> GetByIdAsync(int id);
        Task<TodoItemDto> CreateAsync(CreateItemDto createItemDto);
        Task<bool> UpdateAsync(int id, UpdateItemDto updateItemDto);
        Task<bool> PathAsync(int id);
        Task<bool> DeleteAsync(int id);

    }
}
