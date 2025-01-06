using System;
using System.ComponentModel.DataAnnotations;

namespace Biodigestor.Models
{
    public class Consulta
    {
        [Key]
        public int IdConsulta { get; set; }  // Clave primaria de la consulta
        public required string Titulo { get; set; }  // Título de la consulta
        public required string Contenido { get; set; }  // Contenido de la consulta
        public required string Username { get; set; }  // Nombre del usuario que hizo la consulta
        public DateTime FechaCreacion { get; set; }  // Fecha de creación de la consulta

        public required ICollection<Respuesta> Respuestas { get; set; } 
    }
}
