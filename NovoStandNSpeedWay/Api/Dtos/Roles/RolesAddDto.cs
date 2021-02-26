﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Roles
{
    public class RolesAddDto
    {
        public string DescripcionVar { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public DateTime FechaModDate { get; set; } = DateTime.Now;
        public int UsuarioIdInt { get; set; }
        public int UsuarioIdModInt { get; set; } = 0;
        public bool? ActivoBit { get; set; }

    }
}
