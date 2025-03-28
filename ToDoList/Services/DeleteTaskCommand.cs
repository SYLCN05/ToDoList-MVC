using ToDoList.Interfaces;
using ToDoList.Models;
namespace ToDoList.Services
{
    public class DeleteTaskCommand : ICommandable
    {
        private readonly ApiFacade _apiFacade;
        private readonly int? _taakId;

        public DeleteTaskCommand(ApiFacade facade, int? taakId)
        {
            _apiFacade = facade;
            _taakId = taakId;
        }
        public async Task ExecuteAsync()
        {
            await _apiFacade.DeleteTaak(_taakId);
        }
    }
}
