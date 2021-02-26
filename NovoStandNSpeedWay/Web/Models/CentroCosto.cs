using System;
using System.Collections.Generic;


namespace Web.Models
{
    public partial class CentroCosto
    {
        public CentroCosto()
        {
            Activos = new HashSet<Activo>();
        }

        public string CentroCostosIdVar { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime FechaAltaDate { get; set; } = DateTime.Now;
        public DateTime FechaModDate { get; set; } = Convert.ToDateTime("1900-01-01");
        public int UsuarioIdInt { get; set; }
        public int UsuarioIdModInt { get; set; } = 0;
        public bool? ActivoBit { get; set; }

        public virtual ICollection<Activo> Activos { get; set; }
    }
}
