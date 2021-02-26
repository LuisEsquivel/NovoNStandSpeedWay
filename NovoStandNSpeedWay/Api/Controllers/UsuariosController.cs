using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Usuarios;
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
        [Route("api/usuarios/")]
        [ApiController]
        [ApiExplorerSettings(GroupName = "ApiUsuarios")]
    public class UsuariosController : Controller
    {

        private IGenericRepository<Usuario> repository;
            private IMapper mapper;
            private Response response;


            public UsuariosController(ApplicationDbContext context, IMapper _mapper)
            {
                this.mapper = _mapper;
                this.repository = new GenericRepository<Usuario>(context);
                this.response = new Response();
            }


            /// <summary>
            ///Usuarios Get
            /// </summary>
            /// <returns>lista de usuarios</returns>
            [HttpGet("get")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult Get()
            {

                var list = repository.GetAll();

                var listDto = new List<UsuarioDto>();

                foreach (var row in list)
                {
                    listDto.Add(mapper.Map<UsuarioDto>(row));
                }

                return Ok(response.ResponseValues(this.Response.StatusCode, listDto));

            }



            /// <summary>
            /// Obtener usuario por el Id
            /// </summary>
            /// <param name="Id"></param>
            /// <returns>StatusCode 200</returns>
            [HttpGet("GetById/{Id:int}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult GetById(int Id)
            {
                var row = this.repository.GetById(Id);
                var listDto = new List<UsuarioDto>();
                listDto.Add(mapper.Map<UsuarioDto>(row));
                return Ok(this.response.ResponseValues(this.Response.StatusCode, listDto));
            }





            /// <summary>
            /// Agregar un nuevo usuario
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPost("Add")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Add([FromBody] UsuarioAddDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }


                if (repository.Exist(x => x.NombreVar == dto.NombreVar))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El registro Ya Existe!!"));
                }

                var usuario = mapper.Map<Usuario>(dto);
                usuario.FechaAltaDate = DateTime.Now;
                usuario.FechaModDate = Convert.ToDateTime ("1900-01-01");
                usuario.UsuarioIdModInt = 0;

                if (!repository.Add(usuario))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal guardar el registro: {dto.NombreVar}"));
                }

                return Ok(
                             response.ResponseValues(this.Response.StatusCode,
                                                     mapper.Map<UsuarioDto>(repository.GetById(usuario.UsuarioIdInt))
                                                   )
                          );
            }



            /// <summary>
            /// Actualizar usuario
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPut("Update")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Update([FromBody] UsuarioUpdateDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }

                if (repository.Exist(x => x.NombreVar == dto.NombreVar && x.UsuarioIdInt != dto.UsuarioIdInt))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Registro Ya Existe!!"));
                }

                var usuario = mapper.Map<Usuario>(dto);
                var update = repository.GetByValues(x => x.UsuarioIdInt == dto.UsuarioIdInt).FirstOrDefault();
                usuario.FechaModDate = DateTime.Now;
                usuario.FechaAltaDate = update.FechaAltaDate;
                usuario.UsuarioIdInt = update.UsuarioIdInt;

                if (!repository.Update(usuario , usuario.UsuarioIdInt))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al actualizar el registro: {dto.NombreVar}"));
                }


                return Ok(
                           response.ResponseValues(this.Response.StatusCode,
                                                   mapper.Map<UsuarioDto>(repository.GetById(usuario.UsuarioIdInt))
                                                 )
                        );

            }



            /// <summary>
            /// Eliminar usuario por Id
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


                if (repository.Exist(x => x.UsuarioIdInt == Id))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El registro con Id: {Id} No existe"));
                }

                var row = repository.GetById(Id);

                var usuario  = mapper.Map<Usuario>(row);

                if (!repository.Delete(usuario))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al eliminar el registro: {usuario.NombreVar}"));

                }


                return Ok(response.ResponseValues(this.Response.StatusCode));
            }

        }


    }
