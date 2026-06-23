using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.DTOs.Matricula
{
    public class MatriculaUpdateDTO
    {
        [Required]
        public int IdMatricula { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe indicar un estudiante válido")]
        public int EstudianteId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe indicar un curso válido")]
        public int CursoId { get; set; }
    }

}
