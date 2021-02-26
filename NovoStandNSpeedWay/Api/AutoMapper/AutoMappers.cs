

using Api.Dtos.Usuarios;
using AutoMapper;
using Api.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Roles;
using Api.Dtos.Lectores;
using Api.Dtos.FormaAdquisicion;
using Api.Dtos.Ubicaciones;
using Api.Dtos.CentroDeCostos;
using Api.Dtos.Activos;
using Api.Dtos.Eventos;
using Api.Dtos.EpcProductosRel;

namespace Api.AutoMapper
{
    public class AutoMappers : Profile
    {

        public AutoMappers()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioAddDto>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateDto>().ReverseMap();


            CreateMap<Role, RolesDto>().ReverseMap();
            CreateMap<Role, RolesAddDto>().ReverseMap();
            CreateMap<Role, RolesUpdateDto>().ReverseMap();



            CreateMap<Lectore, LectoresDto>().ReverseMap();
            CreateMap<Lectore, LectoresAddDto>().ReverseMap();
            CreateMap<Lectore, LectoresUpdateDto>().ReverseMap();


            CreateMap<Ubicacione, UbicacionesDto>().ReverseMap();
            CreateMap<Ubicacione, UbicacionesAddDto>().ReverseMap();
            CreateMap<Ubicacione, UbicacionesUpdateDto>().ReverseMap();


            CreateMap<FormaAdquisicion, FormaAdquisicionDto>().ReverseMap();
            CreateMap<FormaAdquisicion, FormaAdquisicionAddDto >().ReverseMap();
            CreateMap<FormaAdquisicion, FormaAdquisicionUpdateDto >().ReverseMap();


            CreateMap<CentroCosto, CentroCostoDto>().ReverseMap();
            CreateMap<CentroCosto, CentroCostoAddDto>().ReverseMap();
            CreateMap<CentroCosto, CentroCostoUpdateDto>().ReverseMap();


            CreateMap<Activo, ActivosDto>().ReverseMap();
            CreateMap<Activo, ActivosAddDto >().ReverseMap();
            CreateMap<Activo, ActivosUpdateDto>().ReverseMap();


            CreateMap<Evento, EventosDto>().ReverseMap();
            CreateMap<Evento, EventosAddDto>().ReverseMap();
            CreateMap<Evento, EventosUpdateDto>().ReverseMap();

            CreateMap<EpcProductosRel, EpcProductosRelDto>().ReverseMap();
            CreateMap<EpcProductosRel, EpcProductosRelAddDto>().ReverseMap();
            CreateMap<EpcProductosRel, EpcProductosRelUpdateDto>().ReverseMap();
        }
    }
}
