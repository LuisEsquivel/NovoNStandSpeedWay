
using System;

namespace Api.Dtos.Lectores
{
    public class LectoresUpdateDto
    {

        public int LectorIdInt { get; set; }
        public string DescripcionVar { get; set; }
        public string DireccionVar { get; set; }
        public string ModeloVar { get; set; }
        public DateTime FechaModDate { get; set; }
        public int UsuarioIdModInt { get; set; }

    }
}
