
using System;

namespace Api.Dtos.Roles
{
    public partial class RolesDto
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
