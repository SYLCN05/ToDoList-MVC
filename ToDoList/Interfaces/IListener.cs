using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface IListener
    {
        string Notify(string type);
    }
}
