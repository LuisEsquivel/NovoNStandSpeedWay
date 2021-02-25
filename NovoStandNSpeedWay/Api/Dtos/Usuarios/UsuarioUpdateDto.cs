using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Usuarios
{
    public class UsuarioUpdateDto
    {

        public int UsuarioIdInt { get; set; }
        public string NombreVar { get; set; }
        public int RolIdInt { get; set; }
        public bool EsAdminBit { get; set; }
        public DateTime FechaModDate { get; set; }
        public int UsuarioRegIdInt { get; set; }
        public bool ActivoBit { get; set; }

    }
}
