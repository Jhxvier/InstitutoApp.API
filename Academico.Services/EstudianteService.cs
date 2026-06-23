using Academico.Common.Exceptions;
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
            if (entity == null)
            {
                throw new InvalidStudentDataException("Debe enviar la información del estudiante");
            }

            if (!Enum.IsDefined(typeof(TipoIdentificacion), entity.TipoIdentificacion))
            {
                throw new InvalidStudentDataException("Debe indicar un tipo de identificación válido");
            }

            if (string.IsNullOrWhiteSpace(entity.Cedula))
            {
                throw new InvalidStudentDataException("La cédula es obligatoria");
            }

            if (string.IsNullOrWhiteSpace(entity.Nombre))
            {
                throw new InvalidStudentDataException("El nombre es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(entity.PrimerApellido))
            {
                throw new InvalidStudentDataException("El primer apellido es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(entity.SegundoApellido))
            {
                throw new InvalidStudentDataException("El segundo apellido es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(entity.CorreoElectronico))
            {
                throw new InvalidStudentDataException("El correo electrónico es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(entity.Telefono))
            {
                throw new InvalidStudentDataException("El teléfono es obligatorio");
            }

            var estudiantes = await _repo.ObtenerTodosAsync();
            if (estudiantes.Any(estudiante =>
                estudiante.Cedula.Trim().Equals(entity.Cedula.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                throw new DuplicateIdentificationException("Ya existe un estudiante con esa cédula");
            }

            if (estudiantes.Any(estudiante =>
                estudiante.CorreoElectronico.Trim().Equals(entity.CorreoElectronico.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                throw new DuplicateStudentEmailException("Ya existe un estudiante con ese correo electrónico");
            }

            var estudianteNuevo = _mapper.Map<Estudiante>(entity);
            estudianteNuevo.Cedula = estudianteNuevo.Cedula.Trim();
            estudianteNuevo.Nombre = estudianteNuevo.Nombre.Trim();
            estudianteNuevo.PrimerApellido = estudianteNuevo.PrimerApellido.Trim();
            estudianteNuevo.SegundoApellido = estudianteNuevo.SegundoApellido.Trim();
            estudianteNuevo.CorreoElectronico = estudianteNuevo.CorreoElectronico.Trim();
            estudianteNuevo.Telefono = estudianteNuevo.Telefono.Trim();
            estudianteNuevo = await _repo.CrearAsync(estudianteNuevo);

            return new Response<EstudianteResponseDTO>
            {
                Success = true,
                Message = "Estudiante creado correctamente",
                Data = _mapper.Map<EstudianteResponseDTO>(estudianteNuevo)
            };
        }

        public async Task<Response<EstudianteResponseDTO>> ActualizarAsync(EstudianteUpdateDTO entity)
        {
            if (entity == null)
            {
                throw new InvalidStudentDataException("Debe enviar la información del estudiante");
            }

            var estudianteActual = await _repo.ObtenerPorIdAsync(entity.IdEstudiante)
                ?? throw new NotFoundException($"El estudiante con el id {entity.IdEstudiante} no existe");

            if (!Enum.IsDefined(typeof(TipoIdentificacion), entity.TipoIdentificacion))
            {
                throw new InvalidStudentDataException("Debe indicar un tipo de identificación válido");
            }

            if (string.IsNullOrWhiteSpace(entity.Cedula))
            {
                throw new InvalidStudentDataException("La cédula es obligatoria");
            }

            if (string.IsNullOrWhiteSpace(entity.Nombre))
            {
                throw new InvalidStudentDataException("El nombre es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(entity.PrimerApellido))
            {
                throw new InvalidStudentDataException("El primer apellido es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(entity.SegundoApellido))
            {
                throw new InvalidStudentDataException("El segundo apellido es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(entity.CorreoElectronico))
            {
                throw new InvalidStudentDataException("El correo electrónico es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(entity.Telefono))
            {
                throw new InvalidStudentDataException("El teléfono es obligatorio");
            }

            var estudiantes = await _repo.ObtenerTodosAsync();
            if (estudiantes.Any(estudiante =>
                estudiante.IdEstudiante != entity.IdEstudiante &&
                estudiante.Cedula.Trim().Equals(entity.Cedula.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                throw new DuplicateIdentificationException("Ya existe un estudiante con esa cédula");
            }

            if (estudiantes.Any(estudiante =>
                estudiante.IdEstudiante != entity.IdEstudiante &&
                estudiante.CorreoElectronico.Trim().Equals(entity.CorreoElectronico.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                throw new DuplicateStudentEmailException("Ya existe un estudiante con ese correo electrónico");
            }

            var estudiante = _mapper.Map<Estudiante>(entity);
            estudiante.Cedula = estudiante.Cedula.Trim();
            estudiante.Nombre = estudiante.Nombre.Trim();
            estudiante.PrimerApellido = estudiante.PrimerApellido.Trim();
            estudiante.SegundoApellido = estudiante.SegundoApellido.Trim();
            estudiante.CorreoElectronico = estudiante.CorreoElectronico.Trim();
            estudiante.Telefono = estudiante.Telefono.Trim();
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
    }


}
