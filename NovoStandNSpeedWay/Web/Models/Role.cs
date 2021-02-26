using System;
using System.Collections.Generic;


namespace Web.Models
{
    public partial class Role
    {
        public int RolIdInt { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime FechaAltaDate { get; set; } 
        public DateTime FechaModDate { get; set; } 
        public int UsuarioIdInt { get; set; }
        public int UsuarioIdModInt { get; set; } 
        public bool? ActivoBit { get; set; }
    }
}
