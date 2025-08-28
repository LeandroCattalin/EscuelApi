using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EscuelApi.Models; // Asegúrate de tener esta directiva
using EscuelApi.Data; // Y esta para el DbContext
using Microsoft.EntityFrameworkCore;
using EscuelApi.Dtos.Profesor; // Asegúrate de tener esta directiva para los DTOs

namespace EscuelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfesoresController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public ProfesoresController(EscuelaContext context)
        {
            _context = context;
        }
        // GET /profesores
        [HttpGet]
        public async Task<ActionResult<List<ProfesorDto>>> GetProfesores()
        {
            // Obtengo todos los profesores de la db
            var profesores = await _context.Profesores.ToListAsync();
            // Mapea la lista de Profesor a una lista de ProfesorDto
            var profesoresDto = profesores.Select(p => new ProfesorDto
            {
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                Edad = p.Edad,
                EscuelaId = p.EscuelaId,
                Asignatura = p.Asignatura,
                Experiencia = p.Experiencia
            }).ToList();
            return Ok(profesoresDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorDto>> GetProfesorById(int id)
        {
            // Obtengo el profesor por id de la db y verifico que exista
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            // Mapeo los valores del profesor de la db a un DTO
            var profesorDto = new ProfesorDto
            {
                Nombre = profesor.Nombre,
                Apellido = profesor.Apellido,
                Edad = profesor.Edad,
                EscuelaId = profesor.EscuelaId,
                Asignatura = profesor.Asignatura,
                Experiencia = profesor.Experiencia
            };
            return Ok(profesorDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateProfesor(CreateProfesorDto nuevoProfesorDto)
        {
            // Creo una nueva instancia de Profesor
            var nuevoProfesor = new Profesor();
            // Obtengo las propiedades de ProfesorDTO para mapearlas
            var propiedades = typeof(CreateProfesorDto).GetProperties();
            // Itero en todas las propiedades
            foreach (var prop in propiedades)
            {
                // Obtengo el valor de la propiedad del DTO
                var valor = prop.GetValue(nuevoProfesorDto);
                if (valor != null)
                {
                    // Obtengo la propiedad correspondiente en el modelo
                    var propProfesor = typeof(Profesor).GetProperty(prop.Name);
                    if (propProfesor != null && propProfesor.CanWrite)
                    {
                        // Mapeo el valor al nuevo profesor
                        propProfesor.SetValue(nuevoProfesor, valor);
                    }
                }
            }
            // Agrego el nuevo profesor a la base de datos
            _context.Profesores.Add(nuevoProfesor);
            await _context.SaveChangesAsync();
            // Retorno 201 Created
            return CreatedAtAction(nameof(GetProfesorById), new { id = nuevoProfesor.Id }, nuevoProfesor);
        }
        // PUT /profesores/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProfesor(int id, ProfesorDto UpdateProfesorDto)
        {
            // Obtengo el profesor con el id enviado de la db
            var profesorExistente = await _context.Profesores.FindAsync(id);
            // Verifico si existe
            if (profesorExistente == null)
            {
                return BadRequest();
            }
            // Como no tengo mapper obtengo todas las propiedades por el DTO para iterar sobre todas las propiedades
            var propiedades = typeof(UpdateProfesorDto).GetProperties();
            foreach (var prop in propiedades)
            {
                // Obtengo los valores de las propiedades por las que voy iterando
                var valor = prop.GetValue(UpdateProfesorDto);
                if (valor != null)
                {
                    // Obtengo la propiedad correspondiente en el modelo
                    var propProfesor = typeof(Profesor).GetProperty(prop.Name);
                    // Verifico que pude obtener la propiedad
                    if (propProfesor != null && propProfesor.CanWrite)
                    {
                        // Seteo el nuevo valor y seteo el context para que se actualice
                        propProfesor.SetValue(profesorExistente, valor);
                        _context.Entry(profesorExistente).State = EntityState.Modified;
                    }
                }
            }
            // Guardo, hago catch de errores de la db y retorno ok
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

            return Ok();
        }
        // PATCH en /profesores/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateProfesor(int id, UpdateProfesorDto updateProfesorDto)
        {
            // Obtengo el profesor con el id enviado de la db
            var profesorExistente = await _context.Profesores.FindAsync(id);
            // Verifico si existe
            if (profesorExistente == null)
            {
                return NotFound();
            }
            // Obtengo las propiedades del DTO para poder iterar porque no tengo mappers todavia
            var propiedades = typeof(UpdateProfesorDto).GetProperties();
            foreach (var prop in propiedades)
            {
                // Obtengo el valor de cada propiedad de los datos pasados
                var valor = prop.GetValue(updateProfesorDto);
                if (valor != null)
                {
                    // Obtengo la variable correspondiente en el modelo
                    var propProfesor = typeof(Profesor).GetProperty(prop.Name);
                    if (propProfesor != null && propProfesor.CanWrite)
                    {
                        // Actualizo el valor y seteo el context de que se tiene que actualizar
                        propProfesor.SetValue(profesorExistente, valor);
                        _context.Entry(profesorExistente).State = EntityState.Modified;
                    }
                }
            }
            // Guardo los cambios en la base de datos y catcheo errores
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Profesores.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Retorno 200
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProfesor(int id)
        {
            // Obtengo el profesor con el id enviado de la db
            var profesorExistente = await _context.Profesores.FindAsync(id);
            // Verifico si existe
            if (profesorExistente == null)
            {
                return NotFound();
            }
            // Elimino el profesor
            _context.Profesores.Remove(profesorExistente);
            // Guardo los cambios en la base de datos y catcheo errores
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Profesores.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Retorno 200
            return Ok();
        }
    }
}