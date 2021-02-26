using System;
using System.Collections.Generic;


namespace Web.Models
{
    public partial class Usuario
    {
        public int UsuarioIdInt { get; set; }
        public string NombreVar { get; set; }
        public int RolIdInt { get; set; }
        public bool EsAdminBit { get; set; }
        public DateTime FechaAltaDate { get; set; } = DateTime.Now;
        public DateTime FechaModDate { get; set; } = Convert.ToDateTime("1900-01-01");
        public int UsuarioRegIdInt { get; set; }
        public int UsuarioIdModInt { get; set; } = 0;
        public bool ActivoBit { get; set; }
    }
}
