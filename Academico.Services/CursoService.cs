using Academico.Common.Interfaces;
using Academico.Common.Responses;
using Academico.Entities;
using AutoMapper;
using inaApp.Common.Exceptions;
using inaApp.DTOs.Curso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Academico.Common.Enums.Enums;

namespace Academico.Services
{
    public class CursoService : IGenericService<CursoResponseDTO, CursoCreateDTO, CursoUpdateDTO>
    {
        private readonly IGenericRepository<Curso> _repo;
        private readonly IMapper _mapper;

        public CursoService(IGenericRepository<Curso> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Response<CursoResponseDTO>> CrearAsync(CursoCreateDTO entity)
        {
            await ValidarAsync(entity);

            var curso = _mapper.Map<Curso>(entity);
            Normalizar(curso);
            curso = await _repo.CrearAsync(curso);

            return new Response<CursoResponseDTO>
            {
                Success = true,
                Message = "Curso creado correctamente",
                Data = _mapper.Map<CursoResponseDTO>(curso)
            };
        }

        public async Task<Response<CursoResponseDTO>> ActualizarAsync(CursoUpdateDTO entity)
        {
            var cursoActual = await _repo.ObtenerPorIdAsync(entity.IdCurso)
                ?? throw new NotFoundException($"El curso con el id {entity.IdCurso} no existe");

            await ValidarAsync(entity, entity.IdCurso);

            var curso = _mapper.Map<Curso>(entity);
            Normalizar(curso);
            curso.Estado = cursoActual.Estado;
            curso.FechaCreacion = cursoActual.FechaCreacion;
            curso = await _repo.ActualizarAsync(curso);

            return new Response<CursoResponseDTO>
            {
                Success = true,
                Message = "Curso actualizado correctamente",
                Data = _mapper.Map<CursoResponseDTO>(curso)
            };
        }

        public async Task<Response<bool>> EliminarAsync(int id)
        {
            var eliminado = await _repo.EliminarAsync(id);

            if (!eliminado)
            {
                throw new NotFoundException($"El curso con el id {id} no existe");
            }

            return new Response<bool>
            {
                Success = true,
                Message = "Curso eliminado correctamente",
                Data = eliminado
            };
        }

        public async Task<Response<CursoResponseDTO>> ObtenerPorIdAsync(int id)
        {
            var curso = await _repo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException($"El curso con el id {id} no existe");

            return new Response<CursoResponseDTO>
            {
                Success = true,
                Message = "Curso obtenido correctamente",
                Data = _mapper.Map<CursoResponseDTO>(curso)
            };
        }

        public async Task<Response<List<CursoResponseDTO>>> ObtenerTodosAsync()
        {
            var cursos = await _repo.ObtenerTodosAsync();

            return new Response<List<CursoResponseDTO>>
            {
                Success = true,
                Message = "Consulta realizada correctamente",
                Data = _mapper.Map<List<CursoResponseDTO>>(cursos)
            };
        }

        private async Task ValidarAsync(CursoCreateDTO dto, int? id = null)
        {
            if (dto == null)
            {
                throw new ArgumentException("Debe enviar la información del curso");
            }

            if (!Enum.IsDefined(typeof(ModalidadCurso), dto.ModalidadCurso))
            {
                throw new ArgumentException("Debe indicar una modalidad válida para el curso");
            }

            if (string.IsNullOrWhiteSpace(dto.NombreCurso))
            {
                throw new ArgumentException("El nombre del curso es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
            {
                throw new ArgumentException("La descripción del curso es obligatoria");
            }

            if (dto.CupoMaximo <= 0)
            {
                throw new ArgumentException("El cupo máximo debe ser mayor que cero");
            }

            var cursos = await _repo.ObtenerTodosAsync();
            if (cursos.Any(curso =>
                (!id.HasValue || curso.IdCurso != id.Value) &&
                curso.NombreCurso.Trim().Equals(dto.NombreCurso.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Ya existe un curso con ese nombre");
            }
        }

        //metodo externo para limpiar datos con espacios en blanco
        private static void Normalizar(Curso curso)
        {
            curso.NombreCurso = curso.NombreCurso.Trim();
            curso.Descripcion = curso.Descripcion.Trim();
        }
    }

}
