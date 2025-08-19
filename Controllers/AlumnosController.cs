using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EscuelApi.Models; // Asegúrate de tener esta directiva
using EscuelApi.Data; // Y esta para el DbContext
using Microsoft.EntityFrameworkCore;
using EscuelApi.Dtos.Alumno; // Asegúrate de tener esta directiva para los DTOs

namespace EscuelApi.Controllers
{
    // Le indicamos a ASP.NET que esta clase es un controlador API
    [ApiController]
    [Route("[controller]")]
    // Se utiliza con la url /alumnos ASP.NET saca el controller por convencion
    public class AlumnosController : ControllerBase
    {
        // Guardo en una variable el contexto de la base de datos
        private readonly EscuelaContext _context;
        // Constructor

        public AlumnosController(EscuelaContext context)
        {
            _context = context;
        }

        // Obtengo todos los alumnos en el GET /alumnos
        [HttpGet]
        // Funcion asyncrona para obtener todos los alumnos
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            return await _context.Alumnos.ToListAsync();
        }

        // Obtengo todos los alumnos que tenga el mismo IdEscuela
        [HttpGet("escuela/{IdEscuela}")]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnosByEscuela(int IdEscuela)
        {
            // Utiliza LINQ para filtrar los alumnos por EscuelaId
            var alumnos = await _context.Alumnos
                                        .Where(a => a.IdEscuela == IdEscuela)
                                        .ToListAsync();
            // Verificamos si se encontraron alumnos
            if (alumnos == null || !alumnos.Any())
            {
                return NotFound();
            }
            // Devolvemos los resultados
            return alumnos;
        }
        // Post para agregar un alumno en /alumnos
        [HttpPost]
        // Funcion asyncrona para crear alumno recibiendo de parametro un JSON con la estructura de Alumno
        public async Task<ActionResult<Alumno>> CreateAlumno(Alumno alumno)
        {
            // Verificamos que el objeto no sea nulo
            if (alumno == null)
            {
                return BadRequest();
            }
            // Agrego al context el alumno que me pasaron como parametro
            _context.Alumnos.Add(alumno);
            // Guardo en la db los cambios
            await _context.SaveChangesAsync();
            // Devuelvo el alumno creado
            return CreatedAtAction(nameof(GetAlumnos), new { id = alumno.Id }, alumno);
        }
        // Patch para editar alumnos en /alumnos/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAlumno(int id, UpdateAlumnoDto alumno)
        {
            var alumnoExistente = await _context.Alumnos.FindAsync(id);
            if (alumnoExistente == null)
                return NotFound();

            var propiedades = typeof(UpdateAlumnoDto).GetProperties();
            foreach (var prop in propiedades)
            {
                var valor = prop.GetValue(alumno);
                if (valor != null)
                {
                    var propAlumno = typeof(Alumno).GetProperty(prop.Name);
                    if (propAlumno != null && propAlumno.CanWrite)
                    {
                        propAlumno.SetValue(alumnoExistente, valor);
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Alumnos.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno(int id, UpdateAlumnoDto alumno)
        {
            var alumnoExistente = await _context.Alumnos.FindAsync(id);
            if (alumnoExistente == null)
            {
                return BadRequest();
            }
            var propiedades = typeof(UpdateAlumnoDto).GetProperties();
            foreach (var prop in propiedades)
            {
                var valor = prop.GetValue(alumno);
                if (valor != null)
                {
                    var propAlumno = typeof(Alumno).GetProperty(prop.Name);
                    if (propAlumno != null && propAlumno.CanWrite)
                    {
                        propAlumno.SetValue(alumnoExistente, valor);
                        _context.Entry(alumnoExistente).State = EntityState.Modified;
                    }
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Alumnos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // Eliminar un alumno por Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}