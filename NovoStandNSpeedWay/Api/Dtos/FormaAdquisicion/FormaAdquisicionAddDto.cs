using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Dtos.FormaAdquisicion
{
    public partial class FormaAdquisicionAddDto
    {
  

        public string DescripcionVar { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public int UsuarioIdInt { get; set; }
        public bool? ActivoBit { get; set; }


    }
}
