using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Models;
// via deze controller heb ik een api opgesteld die json data returned
namespace ToDoList.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaakController : ControllerBase
    {

        private readonly ToDoListDBContext _context;
        public TaakController(ToDoListDBContext context)
        {
            _context = context;
        }


        [HttpGet("taakitems")]
        public ActionResult<IEnumerable<Taak>> GetTaken()
        {
            var taken = _context.Taken.ToList();
            return Ok(taken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Taak>> GetTaak(int id)
        {
            var taak = await _context.Taken.FindAsync(id);

            if (taak == null)
            {
                return NotFound();
            }
            return Ok(taak);
        }

        [HttpPost]
        public ActionResult<Taak> PostTaak(Taak taak)
        {
            if (ModelState.IsValid)
            {
                _context.Taken.Add(taak);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTaak(int id, Taak taak)
        {
            if (id != taak.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _context.Taken.Update(taak);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTaak(int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var task= _context.Taken.Find(id);

                if(task == null)
                {
                    return NotFound();
                }

                _context.Taken.Remove(task);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
