﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Dtos.Eventos
{
    public partial class EventosUpdateDto
    {
        public long EventoIdInt { get; set; }
        public string EpcVar { get; set; }
        public DateTime FechaDate { get; set; }
        public int LectorIdInt { get; set; }
        public int PuertoInt { get; set; }
        public int ContadorInt { get; set; }
    }
}
