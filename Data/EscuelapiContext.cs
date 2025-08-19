using Microsoft.EntityFrameworkCore;
using EscuelApi.Models; // Asegúrate de usar el namespace de tus modelos

namespace EscuelApi.Data
{
    public class EscuelaContext : DbContext
    {
        public EscuelaContext(DbContextOptions<EscuelaContext> options)
            : base(options)
        {
        }

        // Esta es la propiedad que representa tu tabla en la base de datos
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Escuela> Escuelas { get; set; }
    }
}