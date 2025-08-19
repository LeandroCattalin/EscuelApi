namespace EscuelApi.Models
{
    public class Alumno : Persona
    {
        public int Matricula { get; set; }
        public string? Carrera { get; set; }
        public Alumno(int id, int idEscuela, string? nombre, string? apellido, int? edad, int matricula, string? carrera)
            : base(id, idEscuela, nombre, apellido, edad)
        {
            Matricula = matricula;
            Carrera = carrera;
        }
    }
}