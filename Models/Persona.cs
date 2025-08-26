namespace EscuelApi.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public int IdEscuela { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int? Edad { get; set; }
        public Persona(int id, int idEscuela, string? nombre, string? apellido, int? edad)
        {
            Id = id;
            IdEscuela = idEscuela;
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
        }
        public Persona() : base()
        {
        }
    }
}