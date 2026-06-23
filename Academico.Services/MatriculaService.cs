using Academico.Common.Exceptions;
using Academico.Common.Interfaces;
using Academico.Common.Responses;
using Academico.DTOs.Matricula;
using Academico.Entities;
using AutoMapper;
using inaApp.Common.Exceptions;
using inaApp.DTOs.Matricula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Services
{
    public class MatriculaService : IGenericService<MatriculaResponseDTO, MatriculaCreateDTO, MatriculaUpdateDTO>
    {
        private readonly IGenericRepository<Matricula> _matriculaRepo;
        private readonly IGenericRepository<Estudiante> _estudianteRepo;
        private readonly IGenericRepository<Curso> _cursoRepo;
        private readonly IMapper _mapper;

        public MatriculaService(
            IGenericRepository<Matricula> matriculaRepo,
            IGenericRepository<Estudiante> estudianteRepo,
            IGenericRepository<Curso> cursoRepo,
            IMapper mapper)
        {
            _matriculaRepo = matriculaRepo;
            _estudianteRepo = estudianteRepo;
            _cursoRepo = cursoRepo;
            _mapper = mapper;
        }

        //CREAR
        public async Task<Response<MatriculaResponseDTO>> CrearAsync(MatriculaCreateDTO entity)
        {
            if (entity == null)
            {
                throw new InvalidEnrollmentDataException("Debe enviar la información de la matrícula");
            }

            if (entity.EstudianteId <= 0)
            {
                throw new InvalidEnrollmentDataException("Debe indicar un estudiante válido");
            }

            if (entity.CursoId <= 0)
            {
                throw new InvalidEnrollmentDataException("Debe indicar un curso válido");
            }

            var estudiante = await _estudianteRepo.ObtenerPorIdAsync(entity.EstudianteId)
                ?? throw new NotFoundException("El estudiante no existe o no está activo");

            var curso = await _cursoRepo.ObtenerPorIdAsync(entity.CursoId)
                ?? throw new NotFoundException("El curso no existe o no está activo");

            var matriculas = await _matriculaRepo.ObtenerTodosAsync();
            if (matriculas.Any(matricula =>
                matricula.EstudianteId == estudiante.IdEstudiante &&
                matricula.CursoId == curso.IdCurso))
            {
                throw new DuplicateEnrollmentException("El estudiante ya está matriculado en este curso");
            }

            if (matriculas.Count(matricula => matricula.CursoId == curso.IdCurso) >= curso.CupoMaximo)
            {
                throw new CourseCapacityExceededException("No hay cupo disponible para este curso");
            }

            var nuevaMatricula = _mapper.Map<Matricula>(entity);
            nuevaMatricula = await _matriculaRepo.CrearAsync(nuevaMatricula);

            return new Response<MatriculaResponseDTO>
            {
                Success = true,
                Message = "Matrícula creada correctamente",
                Data = _mapper.Map<MatriculaResponseDTO>(nuevaMatricula)
            };
        }

        //ACTUALIZAR
        public async Task<Response<MatriculaResponseDTO>> ActualizarAsync(MatriculaUpdateDTO entity)
        {
            var matricula = await _matriculaRepo.ObtenerPorIdAsync(entity.IdMatricula)
                ?? throw new NotFoundException($"La matrícula con el id {entity.IdMatricula} no existe");

            return new Response<MatriculaResponseDTO>
            {
                Success = true,
                Message = "La actualización de matrícula queda disponible para implementación futura; no se realizaron cambios.",
                Data = _mapper.Map<MatriculaResponseDTO>(matricula)
            };
        }

        //ELIMINAR
        public async Task<Response<bool>> EliminarAsync(int id)
        {
            var eliminado = await _matriculaRepo.EliminarAsync(id);

            if (!eliminado)
            {
                throw new NotFoundException($"La matrícula con el id {id} no existe");
            }

            return new Response<bool>
            {
                Success = true,
                Message = "Matrícula eliminada correctamente",
                Data = eliminado
            };
        }

        //OBTENER POR ID
        public async Task<Response<MatriculaResponseDTO>> ObtenerPorIdAsync(int id)
        {
            var matricula = await _matriculaRepo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException($"La matrícula con el id {id} no existe");

            return new Response<MatriculaResponseDTO>
            {
                Success = true,
                Message = "Matrícula obtenida correctamente",
                Data = _mapper.Map<MatriculaResponseDTO>(matricula)
            };
        }

        //OBTENER TODOS
        public async Task<Response<List<MatriculaResponseDTO>>> ObtenerTodosAsync()
        {
            var matriculas = await _matriculaRepo.ObtenerTodosAsync();

            return new Response<List<MatriculaResponseDTO>>
            {
                Success = true,
                Message = "Consulta realizada correctamente",
                Data = _mapper.Map<List<MatriculaResponseDTO>>(matriculas)
            };
        }

    }

}
