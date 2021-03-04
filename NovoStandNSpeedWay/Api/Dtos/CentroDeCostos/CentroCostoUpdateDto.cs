
using System;

namespace Api.Dtos.CentroDeCostos
{
    public partial class CentroCostoUpdateDto
    {

        public string CentroCostosIdVar { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime FechaModDate { get; set; }
        public int UsuarioIdModInt { get; set; }
        public bool? ActivoBit { get; set; }

    }
}
