using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ApiFacade _apiFacade;
        public HomeController(ApiFacade apiFacade)
        {
           _apiFacade = apiFacade;
        }

        public async Task<IActionResult> Index()
        {
            var taken = await _apiFacade.GetAllTakenAsync();
            return View(taken);
        }

        

       
    }
}
