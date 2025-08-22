namespace EscuelApi.Dtos.Profesor
{
    public class CreateProfesorDto
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int? Edad { get; set; }
        public int? IdEscuela { get; set; }
        public string? Especialidad { get; set; }
    }
}