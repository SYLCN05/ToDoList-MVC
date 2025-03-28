using ToDoList.Interfaces;

namespace ToDoList.Services
{
    public class TaakDeleteListener : IListener
    {
        public string Notify(string type)
        {
            if(type != null && type.Equals("Delete"))
            {
                string bericht = "Een taak is verwijderd";
                return bericht;
            }
            return null;
           
        }
    }
}
