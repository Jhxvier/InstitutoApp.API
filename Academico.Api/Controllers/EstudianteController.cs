using Academico.Common.Exceptions;
using Academico.Common.Interfaces;
using inaApp.Common.Exceptions;
using inaApp.DTOs.Estudiante;
using Microsoft.AspNetCore.Mvc;

namespace Academico.Api.Controllers
{
    [ApiController]
    [Route("api/Estudiante")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IGenericService<EstudianteResponseDTO, EstudianteCreateDTO, EstudianteUpdateDTO> _service;

        public EstudiantesController(IGenericService<EstudianteResponseDTO, EstudianteCreateDTO, EstudianteUpdateDTO> service)
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
        public async Task<ActionResult> Post([FromBody] EstudianteCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Created("Estudiante creado", await _service.CrearAsync(dto));
            }
            catch (InvalidStudentDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateIdentificationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateStudentEmailException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EstudianteUpdateDTO dto)
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

                dto.IdEstudiante = id;
                return Ok(await _service.ActualizarAsync(dto));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidStudentDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateIdentificationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateStudentEmailException ex)
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
