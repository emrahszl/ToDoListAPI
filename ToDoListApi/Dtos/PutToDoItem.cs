using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Dtos
{
    public class PutToDoItem
    {
        [Required]
        public string Title { get; set; } = null!;

        public bool Done { get; set; }
    }
}
