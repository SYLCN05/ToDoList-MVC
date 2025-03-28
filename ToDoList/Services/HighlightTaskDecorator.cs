using ToDoList.Observer;
using ToDoList.Interfaces;

using ToDoList.Factories;
namespace ToDoList.Services
{
    public class HighlightTaskDecorator : TaskDecorator, IHighlightableTask
    {
        public override string Title { get => "Highlighted" + " " + base.Title; set => base.Title = value; }
        public bool IsHighlighted { get; set; }

        public HighlightTaskDecorator(ITask task) : base(task)
        {
            if (task is IHighlightableTask highlightable)
            {
                IsHighlighted = highlightable.IsHighlighted;
            }
            else
            {
                // Anders standaard true (aangezien  deze decorator gebruikt wordt om te highlighten)
                IsHighlighted = true;
            }
        }
       

        public override ITask Clone()// deze methode zorgt ervoor dat wanneer een taak gecloned wordt het ook de highlight mee neemt dit vind ik ook een leuke functie
        {
            var clonedTask = _task.Clone();
            return new HighlightTaskDecorator(clonedTask) { IsHighlighted = this.IsHighlighted };
        }
    }
}
