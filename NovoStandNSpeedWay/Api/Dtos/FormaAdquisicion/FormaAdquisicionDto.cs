using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Dtos.FormaAdquisicion
{
    public partial class FormaAdquisicionDto
    {
  

        public int FormaAdquisicionIdInt { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public DateTime FechaModDate { get; set; }
        public int UsuarioIdInt { get; set; }
        public int UsuarioIdModInt { get; set; }
        public bool? ActivoBit { get; set; }


    }
}
