using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Ubicaciones
{
    public class UbicacionesAddDto
    {

        public string UbicacionIdVar { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public int UsuarioIdInt { get; set; }
        public bool? ActivoBit { get; set; }

    }
}
