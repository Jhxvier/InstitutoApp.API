using inaApp.DTOs.Curso;
using inaApp.DTOs.Estudiante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.DTOs.Matricula
{
    public class MatriculaResponseDTO
    {
        public int IdMatricula { get; set; }
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public EstudianteResponseDTO Estudiante { get; set; }
        public CursoResponseDTO Curso { get; set; }
    }
}

