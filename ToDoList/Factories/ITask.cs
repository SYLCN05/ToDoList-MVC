namespace ToDoList.Factories
{
    public interface ITask
    {
        int Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        DateTime CreatedDateTime { get; set; }
    }
}
