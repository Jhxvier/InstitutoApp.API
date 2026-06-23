using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.DTOs.Estudiante
{
    public class EstudianteUpdateDTO : EstudianteCreateDTO //update hereda campos de Create
    {
        [Required]
        public int IdEstudiante { get; set; }
    }
}

