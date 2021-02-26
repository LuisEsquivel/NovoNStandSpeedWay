using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Dtos.FormaAdquisicion
{
    public partial class FormaAdquisicionUpdateDto
    {
  

        public int FormaAdquisicionIdInt { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime FechaModDate { get; set; }
        public int UsuarioIdModInt { get; set; }
        public bool? ActivoBit { get; set; }


    }
}
