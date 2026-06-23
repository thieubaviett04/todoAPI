using System.ComponentModel.DataAnnotations;
namespace TodoAPI.DTOs
{
    public class CreateItemDto
    {
        [Required(ErrorMessage ="Tên công việc không được để trống")]
        [StringLength(100, ErrorMessage = "Tên công việc không được quá 100 ký tự")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả công việc không được quá 500 ký tự")]
        public string? Description { get; set; }    
    }
}
