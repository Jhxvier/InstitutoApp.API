using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Entities
{
    [Table("tb_Matricula")]
    public class Matricula
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMatricula { get; set; }

        [Required]
        public int EstudianteId { get; set; }

        [Required]
        public int CursoId { get; set; }

        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
