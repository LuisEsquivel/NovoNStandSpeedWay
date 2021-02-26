

using Api.Dtos.Usuarios;
using AutoMapper;
using Api.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Roles;
using Api.Dtos.Lectores;

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



            CreateMap<Role, LectoresDto>().ReverseMap();
            CreateMap<Role, LectoresAddDto>().ReverseMap();
            CreateMap<Role, LectoresUpdateDto>().ReverseMap();
        }
    }
}
