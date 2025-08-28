namespace EscuelApi.Models
{
    public class Profesor : Persona
    {
        public string Asignatura { get; set; } = string.Empty;
        public int? Experiencia { get; set; }
        public Profesor(int id, int escuelaId, string? nombre, string? apellido, int? edad, string asignatura, int? experiencia)
            : base(id, escuelaId, nombre, apellido, edad)
        {
            Asignatura = asignatura;
            Experiencia = experiencia;
        }
        public Profesor() : base()
        {
        }
    }
}