using EscuelApi.Dtos.Alumno;
using EscuelApi.Dtos.Profesor;

namespace EscuelApi.Dtos.Escuela
{
    public class CreateEscuelaDto
    {
        public string? Nombre { get; set; }
        public string? Ubicacion { get; set; }
    }
}