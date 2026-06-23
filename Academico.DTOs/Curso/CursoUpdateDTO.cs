using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.DTOs.Curso
{
    public class CursoUpdateDTO : CursoCreateDTO //update hereda campos de Create
    {
        [Required]
        public int IdCurso { get; set; }
    }
}

