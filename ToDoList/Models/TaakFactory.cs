using ToDoList.Factories;
using static ToDoList.Models.Taak;

namespace ToDoList.Models
{
    public static class TaakFactory
    {

        public static ITask CreateTask(TaakType type)
        {
            switch (type) 
            {
                case TaakType.Normaal:
                    return new Taak { Type = TaakType.Normaal };
                case TaakType.Urgent:
                    return new Taak { Type = TaakType.Urgent };
                case TaakType.Onzeker:
                    return new Taak { Type= TaakType.Onzeker };
                    
                default:
                    throw new ArgumentException("ongeldige taak type");
            }
        }
    }
}
