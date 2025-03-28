using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToDoList.Data;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TaakEditListener :IListener
    {

        public string Notify(string type)
        {
           if(type != null && type.Equals("Edit"))
            {

                string bericht = "Een taak is geupdate";
                return bericht;
              
            }

            return null;
        }



    }
}
