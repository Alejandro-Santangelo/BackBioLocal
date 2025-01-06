using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Biodigestor.Models;

public class Respuesta
{
    [Key]
    public int IdRespuesta { get; set; }  // Clave primaria de la respuesta

    public required string Contenido { get; set; }  // Contenido de la respuesta

    public int IdConsulta { get; set; }  // Clave foránea de la consulta a la que responde

    public required string Username { get; set; }  // Nombre del usuario que hizo la respuesta

    public DateTime FechaCreacion { get; set; }  // Fecha de creación de la respuesta

    // Relación con la tabla Consulta
    [ForeignKey("IdConsulta")]
    [JsonIgnore]
    public Consulta? Consulta { get; set; }  // Hacer que sea nullable para evitar problemas de null
}
