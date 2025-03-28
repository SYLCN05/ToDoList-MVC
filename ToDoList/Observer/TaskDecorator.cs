using ToDoList.Factories;
using ToDoList.Interfaces;

namespace ToDoList.Observer
{
    // is bedoeld voor de taak highlight alleen heb ik het per ongeluk in de verkeerde map gezet
    public class TaskDecorator : ITask, IHighlightableTask
    {
        protected ITask _task;

        public TaskDecorator(ITask task)
        {
            _task = task;
        }

        public virtual int Id
        {
            get => _task.Id;
            set => _task.Id = value;
        }

        public virtual string Title
        {
            get => _task.Title;
            set => _task.Title = value;
        }

        public virtual string Description
        {
            get => _task.Description;
            set => _task.Description = value;
        }

        public virtual DateTime CreatedDateTime
        {
            get => _task.CreatedDateTime;
            set => _task.CreatedDateTime = value;
        }
        public bool IsHighlighted { get ; set ; }

        public virtual ITask Clone()
        {
            return _task.Clone();
        }
    }
}
