using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Academico.Common.Enums.Enums;

namespace inaApp.DTOs.Curso
{
    public class CursoCreateDTO
    {
        [Required(ErrorMessage = "La modalidad del curso es obligatoria")]
        public ModalidadCurso ModalidadCurso { get; set; }

        [Required(ErrorMessage = "El nombre del curso es obligatorio")]
        [StringLength(50)]
        public string NombreCurso { get; set; }

        [Required(ErrorMessage = "La descripción del curso es obligatoria")]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El Cupo Máximo del curso no puede ser negativo")]
        public int CupoMaximo { get; set; }
    }
}

