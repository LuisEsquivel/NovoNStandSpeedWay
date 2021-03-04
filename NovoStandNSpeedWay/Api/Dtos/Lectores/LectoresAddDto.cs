
using System;

namespace Api.Dtos.Lectores
{
    public class LectoresAddDto
    {

        public string DescripcionVar { get; set; }
        public string DireccionVar { get; set; }
        public string ModeloVar { get; set; }
        public DateTime FechaAltaDate { get; set; }
        public int UsuarioIdInt { get; set; }

    }
}
