using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApi.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Title length must be between 1 and 30 characters.")]
        public required string Title { get; set; }
        public bool IsComplete { get; set; } = false;
        [StringLength(255, ErrorMessage = "Description length must be between 1 and 255 characters.")]
        public string? Description { get; set; }

        public TodoItem() { }
        public TodoItem(int id, string title, bool isComplete, string description)
        {
            Id = id;
            Title = title;
            IsComplete = isComplete;
            Description = description;
        }
    }
}
