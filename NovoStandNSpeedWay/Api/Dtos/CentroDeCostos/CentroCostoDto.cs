using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Dtos.CentroDeCostos
{
    public partial class CentroCostoDto
    {

        public string CentroCostosIdVar { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public DateTime FechaModDate { get; set; }
        public int UsuarioIdInt { get; set; }
        public int UsuarioIdModInt { get; set; }
        public bool? ActivoBit { get; set; }

    }
}
