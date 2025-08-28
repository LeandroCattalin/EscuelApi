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
        public async Task<ActionResult<IEnumerable<AlumnoDto>>> GetAlumnos()
        {
            var alumnos = await _context.Alumnos.ToListAsync();
            var alumnosDto = alumnos.Select(p => new AlumnoDto
            {
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                Edad = p.Edad,
                EscuelaId = p.EscuelaId,
                Matricula = p.Matricula,
                Carrera = p.Carrera,
            }).ToList();
            return Ok(alumnosDto);
        }

        // Obtengo todos los alumnos que tenga el mismo EscuelaId
        [HttpGet("escuela/{EscuelaId}")]
        public async Task<ActionResult<IEnumerable<AlumnoDto>>> GetAlumnosByEscuela(int EscuelaId)
        {
            // Utiliza LINQ para filtrar los alumnos por EscuelaId
            var alumnos = await _context.Alumnos
                                        .Where(a => a.EscuelaId == EscuelaId)
                                        .ToListAsync();
            // Verificamos si se encontraron alumnos
            if (alumnos == null || !alumnos.Any())
            {
                return NotFound();
            }
            var alumnosDto = alumnos.Select(p => new AlumnoDto
            {
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                Edad = p.Edad,
                EscuelaId = p.EscuelaId,
                Matricula = p.Matricula,
                Carrera = p.Carrera,
            }).ToList();
            return Ok(alumnosDto);
        }
        // Post para agregar un alumno en /alumnos
        [HttpPost]
        // Funcion asyncrona para crear alumno recibiendo de parametro un JSON con la estructura de Alumno
        public async Task<ActionResult> CreateAlumno(CreateAlumnoDto alumnoDto)
        {
            // Verificamos que el objeto no sea nulo
            if (alumnoDto == null)
            {
                return BadRequest();
            }
            var nuevoAlumno = new Alumno();
            var propiedades = typeof(CreateAlumnoDto).GetProperties();
            foreach (var prop in propiedades)
            {
                var valor = prop.GetValue(alumnoDto);
                if (valor != null)
                {
                    var propAlumno = typeof(Alumno).GetProperty(prop.Name);
                    if (propAlumno != null && propAlumno.CanWrite)
                    {
                        propAlumno.SetValue(nuevoAlumno, valor);
                    }
                }
            }
            _context.Alumnos.Add(nuevoAlumno);
            // Guardo en la db los cambios
            await _context.SaveChangesAsync();
            // Devuelvo el alumno creado
            return CreatedAtAction(nameof(GetAlumnos), new { id = nuevoAlumno.Id }, alumnoDto);
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