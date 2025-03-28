using ToDoList.Factories;

namespace ToDoList.Interfaces
{
    public interface IHighlightableTask:ITask
    {
        bool IsHighlighted { get; set; }
    }
}
