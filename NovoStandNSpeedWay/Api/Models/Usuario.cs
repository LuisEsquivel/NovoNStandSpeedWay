using System;
using System.Collections.Generic;

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
        public string UsuarioVar { get; set; }
        public byte[] PasswordEncryptByte { get; set; }
        public byte[] PasswordKeyByte { get; set; }
        public bool? CuentaVerificadaBit { get; set; }
        public string CodigoDeVerificacionVar { get; set; }
    }
}
