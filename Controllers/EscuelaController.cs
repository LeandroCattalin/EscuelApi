using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EscuelApi.Models; // Asegúrate de tener esta directiva
using EscuelApi.Data; // Y esta para el DbContext
using Microsoft.EntityFrameworkCore;
using EscuelApi.Dtos.Alumno;
using EscuelApi.Dtos.Profesor;
using EscuelApi.Dtos.Escuela; // Asegúrate de tener esta directiva para los DTOs

namespace EscuelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EscuelaController : ControllerBase
    {
        private readonly EscuelaContext _context;
        public EscuelaController(EscuelaContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscuelaDto>>> GetEscuela()
        {
            var escuelas = await _context.Escuelas
                .Include(e => e.Alumnos)
                .Include(e => e.Profesores)
                .ToListAsync();
            var escuelasDto = escuelas.Select(p => new EscuelaDto
            {
                Nombre = p.Nombre,
                Ubicacion = p.Ubicacion,
                Alumnos = p.Alumnos.Select(a => new CreateAlumnoDto
                {
                    Nombre = a.Nombre,
                    Apellido = a.Apellido,
                    Edad = a.Edad,
                    EscuelaId = a.EscuelaId,
                    Matricula = a.Matricula,
                    Carrera = a.Carrera,
                }).ToList(),
                Profesores = p.Profesores.Select(pr => new CreateProfesorDto
                {
                    Nombre = pr.Nombre,
                    Apellido = pr.Apellido,
                    Edad = pr.Edad,
                    EscuelaId = pr.EscuelaId,
                    Asignatura = pr.Asignatura,
                    Experiencia = pr.Experiencia
                }).ToList()
            }).ToList();
            return Ok(escuelasDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateEscuela(CreateEscuelaDto nuevaEscuelaDto)
        {
            var escuela = new Escuela
            {
                Nombre = nuevaEscuelaDto.Nombre,
                Ubicacion = nuevaEscuelaDto.Ubicacion
            };
            _context.Escuelas.Add(escuela);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEscuela), new { id = escuela.Id }, escuela);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEscuela(int id, CreateEscuelaDto escuelaDto)
        {
            var escuela = await _context.Escuelas.FindAsync(id);
            if (escuela == null)
            {
                return NotFound();
            }
            escuela.Nombre = escuelaDto.Nombre;
            escuela.Ubicacion = escuelaDto.Ubicacion;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateEscuela(int id, UpdateEscuelaDto updateEscuelaDto)
        {
            var escuela = await _context.Escuelas.FindAsync(id);
            if (escuela == null)
            {
                return NotFound();
            }
            var escuelaDto = new UpdateEscuelaDto
            {
                Nombre = escuela.Nombre,
                Ubicacion = escuela.Ubicacion
            };
            // Aplico los cambios parciales
            if (updateEscuelaDto.Nombre != null)
            {
                escuela.Nombre = updateEscuelaDto.Nombre;
            }
            if (updateEscuelaDto.Ubicacion != null)
            {
                escuela.Ubicacion = updateEscuelaDto.Ubicacion;
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEscuela(int id)
        {
            var escuela = await _context.Escuelas.FindAsync(id);
            if (escuela == null)
            {
                return NotFound();
            }
            _context.Escuelas.Remove(escuela);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}