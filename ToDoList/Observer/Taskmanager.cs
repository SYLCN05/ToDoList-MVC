using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Observer
{
    public class Taskmanager
    {

        private List<IListener> _listeners = new List<IListener>();

        public void AddListner(IListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(IListener listener)
        {
            _listeners.Remove(listener);
        }

        public string Notify(string type)// krijgt hier de type taak binnen en roept de bijbehorende listener voor die taak type
        {
            string bericht = null;

            foreach(var listener in _listeners)
            {
                var messageResponse = listener.Notify(type);
                if (!string.IsNullOrEmpty(messageResponse))
                {
                    bericht = messageResponse;
                }
                
                
            }
            return bericht;
        }
    }
}
