using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biodigestor.Models;
using System.Linq;
using System.Threading.Tasks;
using Biodigestor.Data;

namespace Biodigestor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestasController : ControllerBase
    {
        private readonly Data.BiodigestorContext _context;

        public RespuestasController(BiodigestorContext context)
        {
            _context = context;
        }

        // GET: api/Respuestas
        [HttpGet]
        public async Task<ActionResult> GetRespuestas()
        {
            var respuestas = await _context.Respuestas.Include(r => r.Consulta).ToListAsync();
            return Ok(respuestas);
        }

        // GET: api/Respuestas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Respuesta>> GetRespuesta(int id)
        {
            var respuesta = await _context.Respuestas.Include(r => r.Consulta).FirstOrDefaultAsync(r => r.IdRespuesta == id);

            if (respuesta == null)
            {
                return NotFound();
            }

            return Ok(respuesta);
        }

        // POST: api/Respuestas
            [HttpPost]
public async Task<ActionResult<Respuesta>> PostRespuesta(RespuestaInputModel inputModel)
{
    // Verificar si la consulta existe
    var consulta = await _context.Consultas.FindAsync(inputModel.IdConsulta);
    if (consulta == null)
    {
        return BadRequest("La consulta asociada no existe.");
    }

    // Asignar valores predeterminados o proporcionados
    string username = "Anónimo";
    if (User?.Identity?.Name != null)
    {
        username = User.Identity.Name;
    }

    var respuesta = new Respuesta
    {
        Contenido = inputModel.Contenido,
        IdConsulta = inputModel.IdConsulta,
        Username = username,
        FechaCreacion = DateTime.Now,
        Consulta = consulta
    };

    _context.Respuestas.Add(respuesta);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetRespuesta", new { id = respuesta.IdRespuesta }, respuesta);
}


        // PUT: api/Respuestas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRespuesta(int id, Respuesta respuesta)
        {
            if (id != respuesta.IdRespuesta)
            {
                return BadRequest();
            }

            _context.Entry(respuesta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Respuestas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRespuesta(int id)
        {
            var respuesta = await _context.Respuestas.FindAsync(id);
            if (respuesta == null)
            {
                return NotFound();
            }

            _context.Respuestas.Remove(respuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
