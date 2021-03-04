

using System;

namespace Api.Dtos.Roles
{
    public class RolesUpdateDto
    {
        public int RolIdInt { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime FechaModDate { get; set; }
        public int UsuarioIdModInt { get; set; }
        public bool? ActivoBit { get; set; }

    }
}
