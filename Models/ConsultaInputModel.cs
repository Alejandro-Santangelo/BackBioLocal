using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ConsultaInputModel
{
    public required string Titulo { get; set; }
    public required  string Contenido { get; set; }
}
