using EscuelApi.Dtos.Alumno;
using EscuelApi.Dtos.Profesor;

namespace EscuelApi.Dtos.Escuela
{
    public class EscuelaDto
    {
        public string? Nombre { get; set; }
        public string? Ubicacion { get; set; }
        public List<CreateAlumnoDto> Alumnos { get; set; } = new List<CreateAlumnoDto>();
        public List<CreateProfesorDto> Profesores { get; set; } = new List<CreateProfesorDto>();
    }
}