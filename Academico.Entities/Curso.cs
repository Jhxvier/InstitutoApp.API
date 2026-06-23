using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Academico.Common.Enums.Enums;

namespace Academico.Entities
{
    [Table("tb_Curso")]
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCurso { get; set; }

        [Required]
        public ModalidadCurso ModalidadCurso { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreCurso { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El Cupo Máximo del curso no puede ser negativo")]
        public int CupoMaximo { get; set; }

        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }


}
