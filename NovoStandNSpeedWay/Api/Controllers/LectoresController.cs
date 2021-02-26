using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Lectores;
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
    /// Lectores Controller
    /// </summary>
    [Route("api/lectores/")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiLectores")]
    public class LectoresController : ControllerBase
    {
        

            private IGenericRepository<Lectore> repository;
            private IMapper mapper;
            private Response response;


            public LectoresController(ApplicationDbContext context, IMapper _mapper)
            {
                this.mapper = _mapper;
                this.repository = new GenericRepository<Lectore>(context);
                this.response = new Response();
            }


            /// <summary>
            ///Lectores Get
            /// </summary>
            /// <returns>lista de lectores</returns>
            [HttpGet("get")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult Get()
            {

                var list = repository.GetAll();

                var listDto = new List<LectoresDto>();

                foreach (var row in list)
                {
                    listDto.Add(mapper.Map<LectoresDto>(row));
                }

                return Ok(response.ResponseValues(this.Response.StatusCode, listDto));

            }



            /// <summary>
            /// Obtener lector por el Id
            /// </summary>
            /// <param name="Id"></param>
            /// <returns>StatusCode 200</returns>
            [HttpGet("GetById/{Id:int}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult GetById(int Id)
            {
                var row = this.repository.GetById(Id);
                var listDto = new List<LectoresDto>();
                listDto.Add(mapper.Map<LectoresDto>(row));
                return Ok(this.response.ResponseValues(this.Response.StatusCode, listDto));
            }





            /// <summary>
            /// Agregar un nuevo lector
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPost("Add")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Add([FromBody] LectoresAddDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }


                if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El registro Ya Existe!!"));
                }

                var lector = mapper.Map<Lectore>(dto);
                lector.FechaModDate = Convert.ToDateTime("1900-01-01");
                lector.UsuarioIdModInt = 0;

                if (!repository.Add(lector))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal guardar el registro: {dto.DescripcionVar}"));
                }

                return Ok(
                             response.ResponseValues(this.Response.StatusCode,
                                                     mapper.Map<LectoresDto>(repository.GetById(lector.LectorIdInt))
                                                   )
                          );
            }



            /// <summary>
            /// Actualizar lector
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPut("Update")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Update([FromBody] LectoresUpdateDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }

                if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar && x.LectorIdInt != dto.LectorIdInt))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Registro Ya Existe!!"));
                }

                var lector = mapper.Map<Lectore>(dto);
                var update = repository.GetByValues(x => x.LectorIdInt == dto.LectorIdInt).FirstOrDefault();
                lector.FechaAltaDate = update.FechaAltaDate;
                lector.UsuarioIdInt = update.UsuarioIdInt;

                if (!repository.Update(lector, lector.LectorIdInt))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al actualizar el registro: {dto.DescripcionVar}"));
                }


                return Ok(
                           response.ResponseValues(this.Response.StatusCode,
                                                   mapper.Map<LectoresDto>(repository.GetById(lector.LectorIdInt))
                                                 )
                        );

            }



            /// <summary>
            /// Eliminar lector por Id
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


                if (repository.Exist(x => x.LectorIdInt == Id))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El registro con Id: {Id} No existe"));
                }

                var row = repository.GetById(Id);

                var lector = mapper.Map<Lectore>(row);

                if (!repository.Delete(lector))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al eliminar el registro: {lector.DescripcionVar}"));

                }


                return Ok(response.ResponseValues(this.Response.StatusCode));
            }

        }
    }
