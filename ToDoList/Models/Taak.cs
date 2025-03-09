using System.ComponentModel.DataAnnotations;
using ToDoList.Factories;
using ToDoList.Models;
namespace ToDoList.Models
{
    public class Taak:ITask
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public TaakType Type { get; set; } 


       
    }
}
