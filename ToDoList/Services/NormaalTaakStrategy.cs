using ToDoList.Factories;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class NormaalTaakStrategy : ITaskStrategy
    {
        public string Execute(Taak taak)
        {
            string bericht = "uitgevoerd met Normaal";
            return bericht;
        }
    }
}
