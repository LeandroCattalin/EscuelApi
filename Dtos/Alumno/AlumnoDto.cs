namespace EscuelApi.Dtos.Alumno
{
    public class AlumnoDto
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int? Edad { get; set; }
        public int? IdEscuela { get; set; }
        public int? Matricula { get; set; }
        public string? Carrera { get; set; }
    }
}