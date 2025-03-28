using System.Windows.Input;
using ToDoList.Interfaces;
using ToDoList.Models;
namespace ToDoList.Services
{
    public class CreateTaskCommand : ICommandable
    {
        private readonly ApiFacade _apiFacade;
        private readonly Taak _taak;

        public CreateTaskCommand(ApiFacade apiFacade, Taak taak)
        {
            _apiFacade = apiFacade;
            _taak = taak;
        }
        public async Task ExecuteAsync()
        {
            await _apiFacade.CreateTaakAsync(_taak);
        }
    }
}
