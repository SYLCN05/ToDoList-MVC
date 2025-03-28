using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class UrgentTaakStrategy : ITaskStrategy
    {
        public string Execute(Taak taak)
        {
            string bericht = "uigevoerd met Urgent";
            return bericht;
        }
    }
}
