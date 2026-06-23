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

        public async Task<Response<MatriculaResponseDTO>> CrearAsync(MatriculaCreateDTO entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Debe enviar la información de la matrícula");
            }

            await ValidarMatriculaAsync(entity.EstudianteId, entity.CursoId);

            var nuevaMatricula = _mapper.Map<Matricula>(entity);
            nuevaMatricula = await _matriculaRepo.CrearAsync(nuevaMatricula);

            return new Response<MatriculaResponseDTO>
            {
                Success = true,
                Message = "Matrícula creada correctamente",
                Data = _mapper.Map<MatriculaResponseDTO>(nuevaMatricula)
            };
        }

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

        //metodo externo para validar una matricula
        private async Task ValidarMatriculaAsync(int estudianteId, int cursoId)
        {
            if (estudianteId <= 0)
            {
                throw new ArgumentException("Debe indicar un estudiante válido");
            }

            if (cursoId <= 0)
            {
                throw new ArgumentException("Debe indicar un curso válido");
            }

            var estudiante = await _estudianteRepo.ObtenerPorIdAsync(estudianteId)
                ?? throw new ArgumentException("El estudiante no existe o no está activo");

            var curso = await _cursoRepo.ObtenerPorIdAsync(cursoId)
                ?? throw new ArgumentException("El curso no existe o no está activo");

            var matriculas = await _matriculaRepo.ObtenerTodosAsync();
            if (matriculas.Any(matricula =>
                matricula.EstudianteId == estudiante.IdEstudiante &&
                matricula.CursoId == curso.IdCurso))
            {
                throw new ArgumentException("El estudiante ya está matriculado en este curso");
            }

            if (matriculas.Count(matricula => matricula.CursoId == curso.IdCurso) >= curso.CupoMaximo)
            {
                throw new ArgumentException("No hay cupo disponible para este curso");
            }
        }
    }

}
