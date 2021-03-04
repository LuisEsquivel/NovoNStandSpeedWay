
using System;

namespace Api.Dtos.Usuarios
{
    public class UsuarioUpdateDto
    {

        public int UsuarioIdInt { get; set; }
        public string NombreVar { get; set; }
        public int RolIdInt { get; set; }
        public bool EsAdminBit { get; set; }
        public DateTime FechaModDate { get; set; }
        public int UsuarioIdModInt { get; set; }
        public bool ActivoBit { get; set; }
        public string UsuarioVar { get; set; }
        public string Password { get; set; }
        public bool? CuentaVerificadaBit { get; set; }
        public string CodigoDeVerificacionVar { get; set; }

    }
}
