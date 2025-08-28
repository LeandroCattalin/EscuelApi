using System.ComponentModel.DataAnnotations.Schema;
namespace EscuelApi.Models
{
    public class Persona
    {
        public int Id { get; set; }
        [ForeignKey("Escuela")]
        public int EscuelaId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int? Edad { get; set; }
        public Persona(int id, int escuelaId, string? nombre, string? apellido, int? edad)
        {
            Id = id;
            EscuelaId = escuelaId;
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
        }
        public Persona() : base()
        {
        }
    }
}