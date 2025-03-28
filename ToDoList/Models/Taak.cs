using System.ComponentModel.DataAnnotations;
using ToDoList.Factories;
using ToDoList.Interfaces;
using ToDoList.Models;
namespace ToDoList.Models
{
    public class Taak:ITask, IHighlightableTask
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public TaakType Type { get; set; }

        public bool IsHighlighted { get; set; }

        public ITask Clone()
        {
            var clone = (Taak)this.MemberwiseClone();
            clone.Id = 0; // Nieuwe Id zodat EF Core een nieuwe record aanmaakt
            clone.Title = Title+" "+"(Kopie)";
            clone.CreatedDateTime = DateTime.Now; // Eventueel ook een nieuwe aanmaakdatum

            return clone;
        }

       
    }
}
