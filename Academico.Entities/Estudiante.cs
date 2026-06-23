using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Academico.Common.Enums.Enums;

namespace Academico.Entities
{
    [Table("tb_Estudiante")]
    public class Estudiante
    {
        //atributos de la entidad estudiante
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEstudiante { get; set; }

        [Required]
        public TipoIdentificacion TipoIdentificacion { get; set; }

        [Required]
        [MaxLength(20)]
        public string Cedula { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string PrimerApellido { get; set; }

        [Required]
        [MaxLength(50)]
        public string SegundoApellido { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string CorreoElectronico { get; set; }

        [Required]
        [MaxLength(20)]
        [Phone]
        public string Telefono { get; set; }

        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }


}
