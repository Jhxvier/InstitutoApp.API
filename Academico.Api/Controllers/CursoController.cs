using Academico.Common.Exceptions;
using Academico.Common.Interfaces;
using inaApp.Common.Exceptions;
using inaApp.DTOs.Curso;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Academico.Api.Controllers
{
    [ApiController]
    [Route("api/Curso")]
    public class CursosController : ControllerBase
    {
        private readonly IGenericService<CursoResponseDTO, CursoCreateDTO, CursoUpdateDTO> _service;

        public CursosController(IGenericService<CursoResponseDTO, CursoCreateDTO, CursoUpdateDTO> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _service.ObtenerTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }

                return Ok(await _service.ObtenerPorIdAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CursoCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Created("Curso creado", await _service.CrearAsync(dto));
            }
            catch (InvalidCourseDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateNameException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CursoUpdateDTO dto)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                dto.IdCurso = id;
                return Ok(await _service.ActualizarAsync(dto));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidCourseDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateNameException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }

                return Ok(await _service.EliminarAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
