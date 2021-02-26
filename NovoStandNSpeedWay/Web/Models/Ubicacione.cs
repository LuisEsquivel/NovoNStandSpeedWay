using System;
using System.Collections.Generic;


namespace Web.Models
{
    public partial class Ubicacione
    {
        public Ubicacione()
        {
            Activos = new HashSet<Activo>();
        }

        public string UbicacionIdVar { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public DateTime FechaModDate { get; set; } 
        public int UsuarioIdInt { get; set; }
        public int UsuarioIdModInt { get; set; } 
        public bool? ActivoBit { get; set; }

        public virtual ICollection<Activo> Activos { get; set; }
    }
}
