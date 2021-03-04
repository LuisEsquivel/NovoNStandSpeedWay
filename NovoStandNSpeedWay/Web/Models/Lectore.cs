using System;

namespace Web.Models
{
    public partial class Lectore
    {
        public int LectorIdInt { get; set; }
        public string DescripcionVar { get; set; }
        public string DireccionVar { get; set; }
        public string ModeloVar { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public DateTime FechaModDate { get; set; } 
        public int UsuarioIdInt { get; set; }
        public int UsuarioIdModInt { get; set; } 
    }
}
