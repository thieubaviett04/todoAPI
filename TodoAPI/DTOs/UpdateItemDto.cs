namespace TodoAPI.DTOs
{
    public class UpdateItemDto
    {
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public string? Description { get; set; }
    }
}
