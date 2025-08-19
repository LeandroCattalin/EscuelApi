namespace EscuelApi.Models
{
    public class Escuela
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Ubicacion { get; set; }
        public List<Alumno> Alumnos { get; set; } = new List<Alumno>();
        public List<Profesor> Profesores { get; set; } = new List<Profesor>();
    }
}