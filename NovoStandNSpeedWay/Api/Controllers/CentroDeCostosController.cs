using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.CentroDeCostos;
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
        /// Centro De Costos Controller
        /// </summary>
        [Route("api/centrodecostos/")]
        [ApiController]
        [ApiExplorerSettings(GroupName = "ApiCentroDeCostos")]
    public class CentroDeCostosController : ControllerBase
    {

        private IGenericRepository<CentroCosto> repository;
            private IMapper mapper;
            private Response response;


            public CentroDeCostosController(ApplicationDbContext context, IMapper _mapper)
            {
                this.mapper = _mapper;
                this.repository = new GenericRepository<CentroCosto>(context);
                this.response = new Response();
            }


            /// <summary>
            ///Centro De Costos Get
            /// </summary>
            /// <returns>lista de roles</returns>
            [HttpGet("get")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult Get()
            {

                var list = repository.GetAll();

                var listDto = new List<CentroCostoDto>();

                foreach (var row in list)
                {
                    listDto.Add(mapper.Map<CentroCostoDto>(row));
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
                var listDto = new List<CentroCostoDto>();
                listDto.Add(mapper.Map<CentroCostoDto>(row));
                return Ok(this.response.ResponseValues(this.Response.StatusCode, listDto));
            }





            /// <summary>
            /// Agregar un nuevo centro de costos
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPost("Add")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Add([FromBody] CentroCostoDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }


                if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El registro Ya Existe!!"));
                }

                var o = mapper.Map<CentroCosto>(dto);
                o.FechaModDate = Convert.ToDateTime("1900-01-01");
                o.UsuarioIdModInt = 0;

                if (!repository.Add(o))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal guardar el registro: {dto.DescripcionVar}"));
                }

                return Ok(
                             response.ResponseValues(this.Response.StatusCode,
                                                     mapper.Map<CentroCostoDto>(repository.GetById(o.CentroCostosIdVar))
                                                   )
                          );
            }



            /// <summary>
            /// Actualizar centro de costos
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPut("Update")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Update([FromBody] CentroCostoUpdateDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }

                if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar && x.CentroCostosIdVar  != dto.CentroCostosIdVar))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Registro Ya Existe!!"));
                }

                var o = mapper.Map<CentroCosto>(dto);
                var update = repository.GetByValues(x => x.CentroCostosIdVar == dto.CentroCostosIdVar).FirstOrDefault();
                o.FechaAltaDate = update.FechaAltaDate;
                o.UsuarioIdInt = update.UsuarioIdInt;



                if (!repository.Update( o, o.CentroCostosIdVar))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al actualizar el registro: {dto.DescripcionVar}"));
                }


                return Ok(
                           response.ResponseValues(this.Response.StatusCode,
                                                   mapper.Map<CentroCostoDto>(repository.GetById(o.CentroCostosIdVar))
                                                 )
                        );

            }



            /// <summary>
            /// Eliminar centro de costos por Id
            /// </summary>
            /// <param name="Id"></param>
            /// <returns>StatusCode 200</returns>
            [HttpDelete("Delete/{Id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Delete(string Id)
            {
                if (Id != null)
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El parámetro (Id) es obligatorio"));
                }


                if (repository.Exist(x => x.CentroCostosIdVar == Id))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El registro con Id: {Id} No existe"));
                }

                var row = repository.GetById(Id);

                var o = mapper.Map<CentroCosto>(row);

                if (!repository.Delete(o))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al eliminar el registro: {o.DescripcionVar}"));

                }


                return Ok(response.ResponseValues(this.Response.StatusCode));
            }

        }

    }
