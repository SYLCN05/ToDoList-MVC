using Newtonsoft.Json;
using ToDoList.Data;
using ToDoList.Models;
namespace ToDoList.Services
{
    public class ApiFacade // opgesteld als een soort simple library voor verschillende soorten methoden die mijn taskcontroller uiteindelijk kan aanroepen
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ApiFacade(HttpClient httpClient, string baseurl= "https://localhost:7290/api/taak")
        {
            _httpClient = httpClient;
            _apiBaseUrl = baseurl;
        }

        public async Task<List<Taak>> GetAllTakenAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/taakitems");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var taken = JsonConvert.DeserializeObject<List<Taak>>(jsonString);
                    return taken;//alle taken returnen naar de gespecificeerde type hierboven bij de deserialize object
                }
                return new List<Taak>();//lege list returnen
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return new List<Taak>();
            }
        }

        public async Task<Taak> GetTaakAsync(int? id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var taak = JsonConvert.DeserializeObject<Taak>(jsonString);
                    return taak; // de bijbehorende taak returnen
                }
                return null;// return null als status code niet succes is
            }
            catch(HttpRequestException ex) 
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
                
        }

        public async Task<(bool Success, string message, Taak CreatedTaak)> CreateTaakAsync(Taak taak)//de methode heb ik zo opgesteld zodat ik in mijn controller kan kijken of succes true is en zo kan ik bepalen of een actie gelukt is
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(taak);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var newTaak = JsonConvert.DeserializeObject<Taak>(jsonString);
                    return (true ,"uw taak is succesvol aangemaakt", newTaak);
                }
                return (false, "er ging iets fout bij het creeren van een taak", null);

            }catch(HttpRequestException ex)
            {
                return (false, $"Request error: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string message)> UpdateTaak(int? id, Taak taak)// zelfde geld hier ook met de bool en message die erbij hoort alleen hier return ik niet een nieuwe taak 
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(taak);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Taak is succesvol bijgewerkt");
                }
                Console.WriteLine("er ging iets fout bij het bewerken van een taak");
                return (false, $"fout bij het bewerken van een taak {response.StatusCode}");

            }
            catch (HttpRequestException ex) 
            {
                return (false, $"request error: {ex.Message}" );
            }
        }

        public async Task<(bool Success, string message)> DeleteTaak(int? id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return (true, "uw taak is succesvol verwijderd");

                }
                return (false, $"er ging iets fout bij het verwijderen van een taak{response.StatusCode}");
            }
            catch(HttpRequestException ex)
            {
                return(false, ex.Message);
            }
           
        }

    }
}
