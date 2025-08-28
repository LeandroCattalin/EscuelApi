namespace EscuelApi.Dtos.Profesor
{
    public class UpdateProfesorDto
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int? Edad { get; set; }
        public int? EscuelaId { get; set; }
        public string? Asignatura { get; set; }
        public int? Experiencia { get; set; }
    }
}