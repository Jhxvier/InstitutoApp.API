using Academico.Common.Exceptions;
using Academico.Common.Interfaces;
using Academico.DTOs.Matricula;
using Academico.Services;
using inaApp.Common.Exceptions;
using inaApp.DTOs.Matricula;
using Microsoft.AspNetCore.Mvc;

namespace Academico.Api.Controllers
{
    [ApiController]
    [Route("api/Matricula")]
    public class MatriculasController : ControllerBase
    {
        private readonly IGenericService<MatriculaResponseDTO, MatriculaCreateDTO, MatriculaUpdateDTO> _service;

        public MatriculasController(IGenericService<MatriculaResponseDTO, MatriculaCreateDTO, MatriculaUpdateDTO> service)
        {
            _service = service;
        }
        //get all
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _service.ObtenerTodosAsync());
        }
        
        //get by id
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
        //post
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MatriculaCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Created("Matricula creada", await _service.CrearAsync(dto));
            }
            catch (InvalidEnrollmentDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateEnrollmentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CourseCapacityExceededException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }


}
