using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biodigestor.Models;
using System.Linq;
using System.Threading.Tasks;
using Biodigestor.Data;
using System;

namespace Biodigestor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly BiodigestorContext _context;

        public ConsultasController(BiodigestorContext context)
        {
            _context = context;
        }

        // GET: api/Consultas
        [HttpGet]
        public async Task<ActionResult> GetConsultas()
        {
            var consultas = await _context.Consultas
                .Include(c => c.Respuestas) // Aseguramos de incluir las respuestas
                .ToListAsync();
            return Ok(consultas);
        }

        // GET: api/Consultas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consulta>> GetConsulta(int id)
        {
            var consulta = await _context.Consultas
                .Include(c => c.Respuestas) // Incluir las respuestas
                .FirstOrDefaultAsync(c => c.IdConsulta == id);

            if (consulta == null)
            {
                return NotFound();
            }

            return Ok(consulta);
        }

        // POST: api/Consultas
        [HttpPost]
        public async Task<ActionResult<Consulta>> PostConsulta([FromBody] ConsultaInputModel consultaInput)
        {
            if (consultaInput == null)
            {
                return BadRequest("Datos inválidos");
            }

            var nuevaConsulta = new Consulta
            {
                Titulo = consultaInput.Titulo,
                Contenido = consultaInput.Contenido,
                FechaCreacion = DateTime.Now,
                Username = User?.Identity?.Name ?? "Anónimo",
                Respuestas = new List<Respuesta>() // Aseguramos que Respuestas no sea null
            };

            _context.Consultas.Add(nuevaConsulta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConsulta), new { id = nuevaConsulta.IdConsulta }, nuevaConsulta);
        }

        // PUT: api/Consultas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsulta(int id, Consulta consulta)
        {
            if (id != consulta.IdConsulta)
            {
                return BadRequest();
            }

            _context.Entry(consulta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("sin-responder")]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultasSinResponder()
        {
            var consultasSinResponder = await _context.Consultas
                .Where(c => !_context.Respuestas.Any(r => r.IdConsulta == c.IdConsulta))
                .ToListAsync();

            return Ok(consultasSinResponder);
        }

        // DELETE: api/Consultas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsulta(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    public class ConsultaInputModel
    {
        public required string Titulo { get; set; }
        public required string Contenido { get; set; }
    }
}
