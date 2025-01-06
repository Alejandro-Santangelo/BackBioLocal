using Biodigestor.DTOs;
using Microsoft.AspNetCore.Mvc;
using Biodigestor.Data;
using Biodigestor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Biodigestor.Controllers;
[Authorize(Roles = "Manager, Tecnico")] 
[ApiController]
[Route("api/[controller]")]
public class SimuladorDeValoresController : ControllerBase
{
    private readonly BiodigestorContext _context;

    public SimuladorDeValoresController(BiodigestorContext context)
    {
        _context = context;
    }

    // POST: api/SimuladorDeValores/Humedad
    [HttpPost("sensor-humedad/{idSensor}")]
    public async Task<IActionResult> PostSensorHumedad(int idSensor, [FromBody] SensorHumedadDto sensorHumedadDto)
    {
        var sensorHumedad = await _context.SensoresHumedad.FindAsync(idSensor);
        if (sensorHumedad == null)
        {
            return NotFound();
        }
        if (sensorHumedadDto.ValorLectura < 0 || sensorHumedadDto.ValorLectura > 100)
        {
            return BadRequest();
        }

        double? alerta = null;
        double? alarma = null;
        double? normal = null;

        // Nueva lógica para humedad
        if (sensorHumedadDto.ValorLectura > 70)
        {
            alarma = sensorHumedadDto.ValorLectura;
        }
        else if (sensorHumedadDto.ValorLectura > 60 && sensorHumedadDto.ValorLectura <= 70 || sensorHumedadDto.ValorLectura < 40)
        {
            alerta = sensorHumedadDto.ValorLectura;
        }
        else
        {
            normal = sensorHumedadDto.ValorLectura;
        }

        var registro = new Registro
        {
            IdSensor = idSensor,
            IdBiodigestor = sensorHumedadDto.IdBiodigestor,
            TipoSensor = "Humedad",
            FechaHora = sensorHumedadDto.FechaHora,
            Alerta = alerta,
            Alarma = alarma,
            Normal = normal
        };

        _context.Registros.Add(registro);
        await _context.SaveChangesAsync();

        return Ok(registro);
    }

    // POST para el sensor de temperatura
    [HttpPost("sensor-temperatura/{idSensor}")]
    public async Task<IActionResult> PostSensorTemperatura(int idSensor, [FromBody] SensorTemperaturaDto sensorTemperaturaDto)
    {
        var sensorTemperatura = await _context.SensoresTemperatura.FindAsync(idSensor);
        if (sensorTemperatura == null)
        {
            return NotFound();
        }

        if (sensorTemperaturaDto.ValorLectura < 0 || sensorTemperaturaDto.ValorLectura > 100)
        {
            return BadRequest();
        } 

        double? alerta = null;
        double? alarma = null;
        double? normal = null;

        // Nueva lógica para temperatura
        if (sensorTemperaturaDto.ValorLectura > 60)
        {
            alarma = sensorTemperaturaDto.ValorLectura;
        }
        else if (sensorTemperaturaDto.ValorLectura > 45 && sensorTemperaturaDto.ValorLectura <= 60 || sensorTemperaturaDto.ValorLectura < 30)
        {
            alerta = sensorTemperaturaDto.ValorLectura;
        }
        else
        {
            normal = sensorTemperaturaDto.ValorLectura;
        }

        var registro = new Registro
        {
            IdSensor = idSensor,
            IdBiodigestor = sensorTemperaturaDto.IdBiodigestor,
            TipoSensor = "Temperatura",
            FechaHora = sensorTemperaturaDto.FechaHora,
            Alerta = alerta,
            Alarma = alarma,
            Normal = normal
        };

        _context.Registros.Add(registro);
        await _context.SaveChangesAsync();

        return Ok(registro);
    }

    // POST para el sensor de presión
    [HttpPost("sensor-presion/{idSensor}")]
    public async Task<IActionResult> PostSensorPresion(int idSensor, [FromBody] SensorPresionDto sensorPresionDto)
    {
        var sensorPresion = await _context.SensoresPresion.FindAsync(idSensor);
        if (sensorPresion == null)
        {
            return NotFound();
        }

        if (sensorPresionDto.ValorLectura < 0 || sensorPresionDto.ValorLectura > 80)
        {
            return BadRequest();
        }

        double? alerta = null;
        double? alarma = null;
        double? normal = null;

        // Nueva lógica para presión
        if (sensorPresionDto.ValorLectura > 25)
        {
            alarma = sensorPresionDto.ValorLectura;
        }
        else if (sensorPresionDto.ValorLectura > 22 && sensorPresionDto.ValorLectura <= 25 || sensorPresionDto.ValorLectura < 10)
        {
            alerta = sensorPresionDto.ValorLectura;
        }
        else
        {
            normal = sensorPresionDto.ValorLectura;
        }

        var registro = new Registro
        {
            IdSensor = idSensor,
            IdBiodigestor = sensorPresionDto.IdBiodigestor,
            TipoSensor = "Presion",
            FechaHora = sensorPresionDto.FechaHora,
            Alerta = alerta,
            Alarma = alarma,
            Normal = normal
        };

        _context.Registros.Add(registro);
        await _context.SaveChangesAsync();

        return Ok(registro);
    }
}
