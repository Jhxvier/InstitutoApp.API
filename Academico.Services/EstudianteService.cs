using Academico.Common.Interfaces;
using Academico.Common.Responses;
using Academico.Entities;
using AutoMapper;
using inaApp.Common.Exceptions;
using inaApp.DTOs.Estudiante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Academico.Common.Enums.Enums;

namespace Academico.Services
{
    public class EstudianteService : IGenericService<EstudianteResponseDTO, EstudianteCreateDTO, EstudianteUpdateDTO>
    {
        private readonly IGenericRepository<Estudiante> _repo;
        private readonly IMapper _mapper;

        public EstudianteService(IGenericRepository<Estudiante> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Response<EstudianteResponseDTO>> CrearAsync(EstudianteCreateDTO entity)
        {
            await ValidarAsync(entity);

            var estudiante = _mapper.Map<Estudiante>(entity);
            Normalizar(estudiante);
            estudiante = await _repo.CrearAsync(estudiante);

            return new Response<EstudianteResponseDTO>
            {
                Success = true,
                Message = "Estudiante creado correctamente",
                Data = _mapper.Map<EstudianteResponseDTO>(estudiante)
            };
        }

        public async Task<Response<EstudianteResponseDTO>> ActualizarAsync(EstudianteUpdateDTO entity)
        {
            var estudianteActual = await _repo.ObtenerPorIdAsync(entity.IdEstudiante)
                ?? throw new NotFoundException($"El estudiante con el id {entity.IdEstudiante} no existe");

            await ValidarAsync(entity, entity.IdEstudiante);

            var estudiante = _mapper.Map<Estudiante>(entity);
            Normalizar(estudiante);
            estudiante.Estado = estudianteActual.Estado;
            estudiante.FechaCreacion = estudianteActual.FechaCreacion;
            estudiante = await _repo.ActualizarAsync(estudiante);

            return new Response<EstudianteResponseDTO>
            {
                Success = true,
                Message = "Estudiante actualizado correctamente",
                Data = _mapper.Map<EstudianteResponseDTO>(estudiante)
            };
        }

        public async Task<Response<bool>> EliminarAsync(int id)
        {
            var eliminado = await _repo.EliminarAsync(id);

            if (!eliminado)
            {
                throw new NotFoundException($"El estudiante con el id {id} no existe");
            }

            return new Response<bool>
            {
                Success = true,
                Message = "Estudiante eliminado correctamente",
                Data = eliminado
            };
        }

        public async Task<Response<EstudianteResponseDTO>> ObtenerPorIdAsync(int id)
        {
            var estudiante = await _repo.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException($"El estudiante con el id {id} no existe");

            return new Response<EstudianteResponseDTO>
            {
                Success = true,
                Message = "Estudiante obtenido correctamente",
                Data = _mapper.Map<EstudianteResponseDTO>(estudiante)
            };
        }

        public async Task<Response<List<EstudianteResponseDTO>>> ObtenerTodosAsync()
        {
            var estudiantes = await _repo.ObtenerTodosAsync();

            return new Response<List<EstudianteResponseDTO>>
            {
                Success = true,
                Message = "Consulta realizada correctamente",
                Data = _mapper.Map<List<EstudianteResponseDTO>>(estudiantes)
            };
        }

        private async Task ValidarAsync(EstudianteCreateDTO dto, int? id = null)
        {
            if (dto == null)
            {
                throw new ArgumentException("Debe enviar la información del estudiante");
            }

            if (!Enum.IsDefined(typeof(TipoIdentificacion), dto.TipoIdentificacion))
            {
                throw new ArgumentException("Debe indicar un tipo de identificación válido");
            }

            if (string.IsNullOrWhiteSpace(dto.Cedula))
            {
                throw new ArgumentException("La cédula es obligatoria");
            }

            if (string.IsNullOrWhiteSpace(dto.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(dto.PrimerApellido))
            {
                throw new ArgumentException("El primer apellido es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(dto.SegundoApellido))
            {
                throw new ArgumentException("El segundo apellido es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(dto.CorreoElectronico))
            {
                throw new ArgumentException("El correo electrónico es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(dto.Telefono))
            {
                throw new ArgumentException("El teléfono es obligatorio");
            }

            var estudiantes = await _repo.ObtenerTodosAsync();
            if (estudiantes.Any(estudiante =>
                (!id.HasValue || estudiante.IdEstudiante != id.Value) &&
                estudiante.Cedula.Trim().Equals(dto.Cedula.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Ya existe un estudiante con esa cédula");
            }

            if (estudiantes.Any(estudiante =>
                (!id.HasValue || estudiante.IdEstudiante != id.Value) &&
                estudiante.CorreoElectronico.Trim().Equals(dto.CorreoElectronico.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Ya existe un estudiante con ese correo electrónico");
            }
        }

        //metodo externo para limpiar datos con espacios en blanco
        private static void Normalizar(Estudiante estudiante)
        {
            estudiante.Cedula = estudiante.Cedula.Trim();
            estudiante.Nombre = estudiante.Nombre.Trim();
            estudiante.PrimerApellido = estudiante.PrimerApellido.Trim();
            estudiante.SegundoApellido = estudiante.SegundoApellido.Trim();
            estudiante.CorreoElectronico = estudiante.CorreoElectronico.Trim();
            estudiante.Telefono = estudiante.Telefono.Trim();
        }
    }

}
