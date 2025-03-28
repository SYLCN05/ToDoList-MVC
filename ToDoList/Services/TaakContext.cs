using ToDoList.Interfaces;
using ToDoList.Models;
namespace ToDoList.Services
{
    public class TaakContext
    {

        private ITaskStrategy _strategy;

        public void SetStrategy(ITaskStrategy strategy)
        {
            _strategy = strategy;
        }
            
        public string ExecuteStrategy(Taak taak)
        {
           return  _strategy.Execute(taak);//returned hier dus de strategy die dus gebruik wordt in de taskcontroller executeTaak
        }
    }
}
