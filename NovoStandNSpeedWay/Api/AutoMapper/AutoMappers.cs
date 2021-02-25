

using Api.Dtos.Usuarios;
using AutoMapper;
using Api.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Roles;

namespace Api.AutoMapper
{
    public class AutoMappers : Profile
    {

        public AutoMappers()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Role , RolesDto>().ReverseMap();
        }
    }
}
