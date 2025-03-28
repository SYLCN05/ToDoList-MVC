using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITaskStrategy
    {
        string Execute (Taak taak);   
    }
}
