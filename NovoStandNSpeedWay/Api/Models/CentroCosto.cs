using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class CentroCosto
    {
        public CentroCosto()
        {
            Activos = new HashSet<Activo>();
        }

        public string CentroCostosIdVar { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public DateTime FechaModDate { get; set; }
        public int UsuarioIdInt { get; set; }
        public int UsuarioIdModInt { get; set; }
        public bool? ActivoBit { get; set; }

        public virtual ICollection<Activo> Activos { get; set; }
    }
}
