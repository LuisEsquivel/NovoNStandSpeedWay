
using System;

namespace Api.Dtos.Ubicaciones
{
    public class UbicacionesUpdateDto
    {

        public string UbicacionIdVar { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime FechaModDate { get; set; }
        public int UsuarioIdModInt { get; set; }
        public bool? ActivoBit { get; set; }

    }
}
