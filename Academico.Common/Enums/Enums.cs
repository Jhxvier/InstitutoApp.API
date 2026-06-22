using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Common.Enums
{
    public static class Enums
    {
        public enum TipoIdentificacion : byte
        {
            CedulaFisica = 1,
            CedulaJuridica = 2,
            DIMEX = 3,
            NITE = 4,
            Pasaporte = 5
        }

        public enum ModalidadCurso : byte
        {
            Presencial = 1,
            Virtual = 2,
            Hibrida = 3
        }
    }
}
