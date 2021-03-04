
using System;

namespace Api.Dtos.Eventos
{
    public partial class EventosUpdateDto
    {
        public long EventoIdInt { get; set; }
        public int PuertoInt { get; set; }
        public string EpcVar { get; set; }
        public string TimestampString { get; set; }
        public int PeakInt { get; set; }
        public int FastIdInt { get; set; }
        public string TidVar { get; set; }
        public string UserMemoryVar { get; set; }
        public DateTime FechaDate { get; set; }
        public int LectorIdInt { get; set; }
        public int ContadorInt { get; set; }
    }
}
