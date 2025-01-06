using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RespuestaInputModel
{
     public int IdConsulta { get; set; }

    public required string Contenido { get; set; } // El único campo requerido en el input
}
