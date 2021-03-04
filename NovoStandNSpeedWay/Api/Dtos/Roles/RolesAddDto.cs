
using System;

namespace Api.Dtos.Roles
{
    public class RolesAddDto
    {
        public string DescripcionVar { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public int UsuarioIdInt { get; set; }
        public bool? ActivoBit { get; set; }

    }
}
