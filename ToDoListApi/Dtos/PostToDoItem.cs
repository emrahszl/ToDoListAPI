using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Dtos
{
    public class PostToDoItem
    {
        [Required]
        public string Title { get; set; } = null!;

        public bool Done { get; set; }
    }
}
