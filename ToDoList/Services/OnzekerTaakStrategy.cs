using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class OnzekerTaakStrategy : ITaskStrategy
    {
        public string Execute(Taak taak)
        {
            string bericht = "uitgevoerd met Onzeker";
            return bericht;
        }
    }
}
