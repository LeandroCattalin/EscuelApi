namespace EscuelApi.Models
{
    public class Profesor : Persona
    {
        public string Asignatura { get; set; }
        public int? Experiencia { get; set; }
        public Profesor(int id, int idEscuela, string? nombre, string? apellido, int? edad, string asignatura, int? experiencia)
            : base(id, idEscuela, nombre, apellido, edad)
        {
            Asignatura = asignatura;
            Experiencia = experiencia;
        }
    }
}