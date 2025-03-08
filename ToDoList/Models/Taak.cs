using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Taak
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
