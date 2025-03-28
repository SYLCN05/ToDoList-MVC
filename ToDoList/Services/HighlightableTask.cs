using ToDoList.Factories;
using ToDoList.Interfaces;

namespace ToDoList.Services
{
    public class HighlightableTask : IHighlightableTask
    {
        private readonly ITask _task;

        public HighlightableTask(ITask task)
        {
            _task = task;
            IsHighlighted = false;  // Een taak is standaard niet highlighted, pas wanneer de user het aangeeft
        }

        public bool IsHighlighted {  get; set; }
        
        public int Id
        {
            get => _task.Id;
            set => _task.Id = value;
        }

        public string Title
        {
            get => _task.Title;
            set => _task.Title = value;
        }

        public string Description
        {
            get => _task.Description;
            set => _task.Description = value;
        }

        public DateTime CreatedDateTime
        {
            get => _task.CreatedDateTime;
            set => _task.CreatedDateTime = value;
        }

        public ITask Clone()
        {
            var clonedTask = _task.Clone();
            var highlightableClone = new HighlightableTask(clonedTask);
            highlightableClone.IsHighlighted = this.IsHighlighted;
            return highlightableClone;
        }
    }
}
