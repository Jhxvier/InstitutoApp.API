using Academico.DTOs.Matricula;
using Academico.Entities;
using AutoMapper;
using inaApp.DTOs.Curso;
using inaApp.DTOs.Estudiante;
using inaApp.DTOs.Matricula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //DE DTOCREATE A ENTITY
            CreateMap<EstudianteCreateDTO, Estudiante>();
            CreateMap<CursoCreateDTO, Curso>();
            CreateMap<MatriculaCreateDTO, Matricula>();
            CreateMap<MatriculaUpdateDTO, Matricula>();

            //DE DTOUPDATE A ENTITY
            CreateMap<EstudianteUpdateDTO, Estudiante>();
            CreateMap<CursoUpdateDTO, Curso>();

            //ENTITY A DTOs Response
            CreateMap<Estudiante, EstudianteResponseDTO>();
            CreateMap<Curso, CursoResponseDTO>();
            CreateMap<Matricula, MatriculaResponseDTO>();
        }
    }

}
