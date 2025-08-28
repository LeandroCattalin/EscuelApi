namespace EscuelApi.Models
{
    public class Alumno : Persona
    {
        public int Matricula { get; set; }
        public string? Carrera { get; set; }
        public Alumno(int id, int escuelaId, string? nombre, string? apellido, int? edad, int matricula, string? carrera)
            : base(id, escuelaId, nombre, apellido, edad)
        {
            Matricula = matricula;
            Carrera = carrera;
        }
        public Alumno()
        {
        }
    }
}