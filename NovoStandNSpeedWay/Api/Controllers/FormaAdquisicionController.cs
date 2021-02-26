using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.FormaAdquisicion;
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
    /// FormaAdquisición Controller
    /// </summary>
    [Route("api/formaadquisicion/")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiFormaAdquisicion")]

    public class FormaAdquisicionController : ControllerBase
    {

            private IGenericRepository<FormaAdquisicion> repository;
            private IMapper mapper;
            private Response response;


            public FormaAdquisicionController(ApplicationDbContext context, IMapper _mapper)
            {
                this.mapper = _mapper;
                this.repository = new GenericRepository<FormaAdquisicion>(context);
                this.response = new Response();
            }


            /// <summary>
            ///Forma Adquisición Get
            /// </summary>
            /// <returns>lista de forma de adquisición</returns>
            [HttpGet("get")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult Get()
            {

                var list = repository.GetAll();

                var listDto = new List<FormaAdquisicionDto>();

                foreach (var row in list)
                {
                    listDto.Add(mapper.Map<FormaAdquisicionDto>(row));
                }

                return Ok(response.ResponseValues(this.Response.StatusCode, listDto));

            }



            /// <summary>
            /// Obtener forma de adquisición por el Id
            /// </summary>
            /// <param name="Id"></param>
            /// <returns>StatusCode 200</returns>
            [HttpGet("GetById/{Id:int}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult GetById(int Id)
            {
                var row = this.repository.GetById(Id);
                var listDto = new List<FormaAdquisicionDto>();
                listDto.Add(mapper.Map<FormaAdquisicionDto>(row));
                return Ok(this.response.ResponseValues(this.Response.StatusCode, listDto));
            }





            /// <summary>
            /// Agregar nueva forma de adquisición
            /// /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPost("Add")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Add([FromBody] FormaAdquisicionAddDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }


                if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El registro Ya Existe!!"));
                }

                var fa = mapper.Map<FormaAdquisicion>(dto);
                fa.FechaAltaDate = DateTime.Now;
                fa.FechaModDate = Convert.ToDateTime("1900-01-01");
                fa.UsuarioIdModInt = 0;

                if (!repository.Add(fa))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal guardar el registro: {dto.DescripcionVar}"));
                }

                return Ok(
                             response.ResponseValues(this.Response.StatusCode,
                                                     mapper.Map<FormaAdquisicionAddDto>(repository.GetById(fa.FormaAdquisicionIdInt))
                                                   )
                          );
            }



            /// <summary>
            /// Actualizar forma de adquisición
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPut("Update")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Update([FromBody] FormaAdquisicionUpdateDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }

                if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar && x.FormaAdquisicionIdInt != dto.FormaAdquisicionIdInt))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Registro Ya Existe!!"));
                }

                var fa = mapper.Map<FormaAdquisicion>(dto);
                var update = repository.GetByValues(x => x.FormaAdquisicionIdInt == dto.FormaAdquisicionIdInt).FirstOrDefault();
                fa.FechaModDate = DateTime.Now;
                fa.FechaAltaDate = update.FechaAltaDate;
                fa.UsuarioIdInt = update.UsuarioIdInt;


                if (!repository.Update(fa, fa.FormaAdquisicionIdInt))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al actualizar el registro: {dto.DescripcionVar}"));
                }


                return Ok(
                           response.ResponseValues(this.Response.StatusCode,
                                                   mapper.Map<FormaAdquisicionUpdateDto>(repository.GetById(fa.FormaAdquisicionIdInt))
                                                 )
                        );

            }



            /// <summary>
            /// Eliminar forma de adquisición por Id
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


                if (repository.Exist(x => x.FormaAdquisicionIdInt  == Id))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El registro con Id: {Id} No existe"));
                }

                var row = repository.GetById(Id);

                var fa = mapper.Map<FormaAdquisicion>(row);

                if (!repository.Delete(fa))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al eliminar el registro: {fa.DescripcionVar}"));

                }


                return Ok(response.ResponseValues(this.Response.StatusCode));
            }

        }
    }
