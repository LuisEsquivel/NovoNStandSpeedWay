using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Usuarios
{
    public class UsuarioAddDto
    {

        public string NombreVar { get; set; }
        public int RolIdInt { get; set; }
        public bool EsAdminBit { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public int UsuarioRegIdInt { get; set; }
        public bool ActivoBit { get; set; }

    }
}
