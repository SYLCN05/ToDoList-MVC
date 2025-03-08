using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToDoList.Data;
using ToDoList.Models;
//deze controller roept mijn api op en gebruikt de gegevens die die returned in  de view
namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ToDoListDBContext _context;
        private readonly HttpClient _httpClient;

        public TaskController(ToDoListDBContext context, HttpClient httpClient)
        { 
            _context= context;
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7290/api/taak/taakitems");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var taken = JsonConvert.DeserializeObject<List<Taak>>(jsonString);
                    return View(taken);
                }
                else
                {
                    return View(new List<Taak>());
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return View(new List<Taak>());
            }
            
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
            try
            {
               
                var jsonContent = JsonConvert.SerializeObject(obj);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                
                var response = await _httpClient.PostAsync("https://localhost:7290/api/taak", content);

                if (response.IsSuccessStatusCode)
                {
                    
                    TempData["Created"] = "Uw taak is succesvol aangemaakt";
                    return RedirectToAction("Index");
                }
                else
                {
                    
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return View(obj);
                }
            }
            catch (HttpRequestException ex)
            {
               
                Console.WriteLine($"Request error: {ex.Message}");
                return View(obj);
            }
        }

        //GET
        public  async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7290/api/taak/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var taak = JsonConvert.DeserializeObject<Taak>(jsonString);
                    return View(taak);
                }
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return View();
            }

            

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Taak obj, int id)
        {
            if (obj == null || id == 0) 
            {
                return NotFound();
            }
            else
            {
                try
                {
                    var jsonContent = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                    var response = await _httpClient.PutAsync($"https://localhost:7290/api/taak/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Edited"] = "uw taak is succesvol geupdate";
                        return RedirectToAction("index");
                    }
                    else
                    {
                        return View(obj);
                    }

                }
                catch (Exception ex) 
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                    return View();  
                }
            }
        }


        //Get
        public async Task<IActionResult> Delete( int? id)
        {
            try            
            {
                var response = await _httpClient.GetAsync($"https://localhost:7290/api/taak/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var taak = JsonConvert.DeserializeObject<Taak>(jsonString);
                    return View(taak);
                }
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Message: {ex.Message}");
                return View();
            }
            
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

                var response = await _httpClient.DeleteAsync($"https://localhost:7290/api/taak/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Deleted"] = "uw taak is succesvol verwijderd";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }


            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error Message: {ex.Message}");
                return View("Index");
            }

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
        
    }
}
