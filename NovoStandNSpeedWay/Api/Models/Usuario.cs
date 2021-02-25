﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class Usuario
    {
        public int UsuarioIdInt { get; set; }
        public string NombreVar { get; set; }
        public int RolIdInt { get; set; }
        public bool EsAdminBit { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public DateTime FechaModDate { get; set; }
        public int UsuarioRegIdInt { get; set; }
        public int UsuarioIdModInt { get; set; }
        public bool ActivoBit { get; set; }
    }
}
