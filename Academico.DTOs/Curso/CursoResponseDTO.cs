using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Academico.Common.Enums.Enums;

namespace inaApp.DTOs.Curso
{
    public class CursoResponseDTO
    {
        public int IdCurso { get; set; }
        public ModalidadCurso ModalidadCurso { get; set; }
        public string NombreCurso { get; set; }
        public string Descripcion { get; set; }
        public int CupoMaximo { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

