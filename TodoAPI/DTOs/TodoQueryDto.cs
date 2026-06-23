namespace TodoAPI.DTOs
{
    public class TodoQueryDto
    {
        public string? SearchTodo { get; set; }
        public bool? IsComplete { get; set; }
        public string? SortBy { get; set; }    
        public string? SortOrder { get; set; }

    }
}
