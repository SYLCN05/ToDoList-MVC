using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToDoList.Data;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Observer;
using ToDoList.Services;
//deze controller roept mijn api op en gebruikt de gegevens die die returned in  de view
namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ToDoListDBContext _context;
        private readonly HttpClient _httpClient;
        private readonly Taskmanager _taskManager;
        private readonly IListener _taskEditListener, _taskDeleteListener;
        private readonly TaakContext _taakContext;
        private readonly ApiFacade _apiFacade;
        public TaskController(ToDoListDBContext context, HttpClient httpClient, Taskmanager taskManager, TaakEditListener taakEditListener, TaakDeleteListener taakDeleteListener,TaakContext taakContext, ApiFacade apifacade )
        { 
            _context= context;
            _httpClient = httpClient;
            _taskManager = taskManager;
            _taskManager.AddListner(taakEditListener);
            _taskManager.AddListner(taakDeleteListener);
            _taakContext = taakContext;
            _apiFacade = apifacade;
     
        }
        public async Task<IActionResult> Index()
        {
           var taken = await _apiFacade.GetAllTakenAsync();
           return View(taken);
            
        }


        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Taak obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var creatTaskCommand = new CreateTaskCommand(_apiFacade, obj);
                    await creatTaskCommand.ExecuteAsync();

                    TempData["Created"] = "uw taak is succesvol aangemaakt";
                    return RedirectToAction("Index");
                }
                catch (Exception ex) 
                {
                    TempData["Error"] = $"er ging iets fout bij het creeren van een taak {ex.Message}";
                    return View(obj);
                }
                
            }
            return BadRequest();
             
        }

        //GET
        public  async Task<IActionResult> Edit(int? id, Taak obj)
        {
            if(id != null)
            {
                var taak = await _apiFacade.GetTaakAsync(id);
                return View(taak);
            }
            TempData["Error"] = "Fout bij het ophalen van de taak die bijgewerkt moet worden";
            return View("Index");
           

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Taak obj, int? id)
        {
            if (obj == null || id == 0)
            {
                return NotFound();
            }

            var UpdatedTaak = await _apiFacade.UpdateTaak(id, obj);

            if (UpdatedTaak.Success)
            {
                TempData["Edited"] = _taskManager.Notify("Edit");
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Fout bij het bijwerken van de taak";
                return View(obj);
            }
           
        }


        //Get
        public async Task<IActionResult> Delete(int id)
        {
            var taak = await _apiFacade.GetTaakAsync(id);
            return View(taak);
            
        }

         //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Taak obj, int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            try
            {
                var deletedTaakCommand = new DeleteTaskCommand(_apiFacade, id);
                await deletedTaakCommand.ExecuteAsync();

                TempData["Deleted"] = _taskManager.Notify("Delete");
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                TempData["Error"] = $"Er ging iets fout bij het verwijderen van een taak {ex.Message}";
            }
            return BadRequest();
            
        }

        //Post
        public IActionResult Search()
        {
            return View();
        }

        public IActionResult SearchTaak(string title)
        {
            return View("Index", _context.Taken.Where(t=> t.Title.Contains(title)));

        }
        
        public IActionResult Clone()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloneTaak(int id)
        {

            if (id <= 0) // Controleert voor een geldige id
            {
                TempData["Error"] = "Ongeldig taak-ID";
                return RedirectToAction("Index");
            }

            try
            {
                var response = await _httpClient.PostAsync($"https://localhost:7290/api/taak/clone/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Cloned"] = "Gelukt! Uw taak is succesvol gecloned!";
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine($"response: {response.ReasonPhrase} - {response.StatusCode}");
                    TempData["Error"] = "Fout bij het klonen van de taak.";
                    return RedirectToAction("Index");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Foutmelding: {ex.Message}");
                TempData["Error"] = "Er is een probleem opgetreden bij de server.";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ExecuteTaak(int id, string strategyType)
        {
            var taak = _context.Taken.Find(id);
            if(taak == null)
            {
                TempData["Error"] = "Taak niet gevonden";
                return RedirectToAction("Index");
            }

            if(strategyType == "Normaal")
            {
                _taakContext.SetStrategy(new NormaalTaakStrategy());

            }
            else if(strategyType == "Urgent")
            {
                _taakContext.SetStrategy(new UrgentTaakStrategy());
            }
            else if(strategyType == "Onzeker")
            {
                _taakContext.SetStrategy(new OnzekerTaakStrategy());
            }

            TempData["Created"] = $"Taak: {taak.Title}  {_taakContext.ExecuteStrategy(taak)} strategie";
            return RedirectToAction("Index");
        }








    }
}
