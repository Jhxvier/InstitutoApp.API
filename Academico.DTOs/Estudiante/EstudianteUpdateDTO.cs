using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.DTOs.Estudiante
{
    public class EstudianteUpdateDTO : EstudianteCreateDTO
    {
        [Required]
        public int IdEstudiante { get; set; }
    }
}

