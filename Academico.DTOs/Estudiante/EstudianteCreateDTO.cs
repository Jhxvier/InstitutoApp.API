using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Academico.Common.Enums.Enums;

namespace inaApp.DTOs.Estudiante
{
    public class EstudianteCreateDTO
    {
        [Required(ErrorMessage = "El tipo de identificación es obligatorio")]
        public TipoIdentificacion TipoIdentificacion { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [MaxLength(20)]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        [MaxLength(50)]
        public string PrimerApellido { get; set; }

        [Required(ErrorMessage = "El segundo apellido es obligatorio")]
        [MaxLength(50)]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        [MaxLength(150)]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [MaxLength(20)]
        [Phone]
        public string Telefono { get; set; }
    }
}

