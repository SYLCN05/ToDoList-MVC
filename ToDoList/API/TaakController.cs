using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Factories;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Services;
// via deze controller heb ik een api opgesteld die json data returned
namespace ToDoList.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaakController : ControllerBase
    {

        private readonly ITaskRepository _repository;
        private readonly ToDoListDBContext _context;
        public TaakController(ITaskRepository repository, ToDoListDBContext context)
        {
            _repository = repository;
            _context = context;
        }


        [HttpGet("taakitems")]
        public async Task<ActionResult<IEnumerable<Taak>>> GetTaken()
        {
            var taken =  await _repository.GetAllAsync();
            return Ok(taken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Taak>> GetTaak(int id)
        {
            var taak = await _repository.GetByIdAsync(id);

            if (taak == null)
            {
                return NotFound();
            }
            return Ok(taak);
        }

        [HttpPost]
        public async Task<ActionResult<Taak>> PostTaak(Taak taak)
        {
            if (taak == null)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                var nieuweTaak = TaakFactory.CreateTask(taak.Type);
                nieuweTaak.Title = taak.Title;
                nieuweTaak.Description = taak.Description;
                nieuweTaak.CreatedDateTime = DateTime.Now;
                nieuweTaak.IsHighlighted = taak.IsHighlighted;

                if (taak.IsHighlighted)
                {
                    var highlightedTaak = new HighlightTaskDecorator(nieuweTaak);
                    nieuweTaak.Title = highlightedTaak.Title;
                }

                var success = await _repository.AddAsync((Taak)nieuweTaak);
                if (success) 
                {
                    return Ok(nieuweTaak);
                }
            }
           
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTaak(int id, Taak taak)
        {
            if (id != taak.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var success = await _repository.UpdateAsync(taak);

                if (success) 
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaak(int id) 
        {

            try
            {
                var success = await _repository.DeleteAsync(id);

                if (success) 
                {
                    return Ok();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpPost("clone/{id}")]
        public ActionResult<Taak> CloneTaak(int id)
        {
            var taak = _context.Taken.Find(id);
            if (taak == null)
            {
                return NotFound();
            }

            var clonedTaak = (Taak)taak.Clone();
            _context.Taken.Add(clonedTaak);
            _context.SaveChanges();
            return Ok(clonedTaak);
        }
    }
}
