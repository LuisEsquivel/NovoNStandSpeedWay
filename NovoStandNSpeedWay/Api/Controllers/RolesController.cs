using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Roles;
using Api.Helpers;
using Api.Interface;
using Api.Models;
using Api.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

        /// <summary>
        /// Usuarios Controller
        /// </summary>
        [Route("api/roles/")]
        [ApiController]
        [ApiExplorerSettings(GroupName = "ApiRoles")]
    public class RolesController : Controller
    {

            private IGenericRepository<Role> repository;
            private IMapper mapper;
            private Response response;


            public RolesController(ApplicationDbContext context, IMapper _mapper)
            {
                this.mapper = _mapper;
                this.repository = new GenericRepository<Role>(context);
                this.response = new Response();
            }


            /// <summary>
            ///Roles Get
            /// </summary>
            /// <returns>lista de roles</returns>
            [HttpGet("get")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult Get()
            {

                var list = repository.GetAll();

                var listDto = new List<RolesDto>();

                foreach (var row in list)
                {
                    listDto.Add(mapper.Map<RolesDto>(row));
                }

                return Ok(response.ResponseValues(this.Response.StatusCode, listDto));

            }



            /// <summary>
            /// Obtener rol por el Id
            /// </summary>
            /// <param name="Id"></param>
            /// <returns>StatusCode 200</returns>
            [HttpGet("GetById/{Id:int}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult GetById(int Id)
            {
                var row = this.repository.GetById(Id);
                var listDto = new List<RolesDto>();
                listDto.Add(mapper.Map<RolesDto>(row));
                return Ok(this.response.ResponseValues(this.Response.StatusCode, listDto));
            }





            /// <summary>
            /// Agregar un nuevo rol
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPost("Add")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Add([FromBody] RolesAddDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }


                if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El registro Ya Existe!!"));
                }

                var rol = mapper.Map<Role>(dto);
                rol.FechaAltaDate = DateTime.Now;
                rol.FechaModDate  = Convert.ToDateTime("1900-01-01");
                rol.UsuarioIdModInt = 0;

                if (!repository.Add(rol))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal guardar el registro: {dto.DescripcionVar}"));
                }

                return Ok(
                             response.ResponseValues(this.Response.StatusCode,
                                                     mapper.Map<RolesDto>(repository.GetById(rol.RolIdInt))
                                                   )
                          );
            }



            /// <summary>
            /// Actualizar rol
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPut("Update")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Update([FromBody] RolesUpdateDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }

                if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar && x.RolIdInt != dto.RolIdInt))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Registro Ya Existe!!"));
                }

                var rol = mapper.Map<Role>(dto);
                var update = repository.GetByValues(x => x.RolIdInt == dto.RolIdInt).FirstOrDefault();
                rol.FechaModDate = DateTime.Now;
                rol.FechaAltaDate = update.FechaAltaDate;
                rol.UsuarioIdInt = update.UsuarioIdInt;



            if (!repository.Update(rol, rol.RolIdInt))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al actualizar el registro: {dto.DescripcionVar}"));
                }


                return Ok(
                           response.ResponseValues(this.Response.StatusCode,
                                                   mapper.Map<RolesDto>(repository.GetById(rol.RolIdInt))
                                                 )
                        );

            }



            /// <summary>
            /// Eliminar rol por Id
            /// </summary>
            /// <param name="Id"></param>
            /// <returns>StatusCode 200</returns>
            [HttpDelete("Delete/{Id:int}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Delete(int Id)
            {
                if (Id <= 0)
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El parámetro (Id) es obligatorio"));
                }


                if (repository.Exist(x => x.RolIdInt == Id))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El registro con Id: {Id} No existe"));
                }

                var row = repository.GetById(Id);

                var rol = mapper.Map<Role>(row);

                if (!repository.Delete(rol))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al eliminar el registro: {rol.DescripcionVar}"));

                }


                return Ok(response.ResponseValues(this.Response.StatusCode));
            }

        }
    }

