using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Ubicaciones;
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
    /// Ubicaciones Controller
    /// </summary>
    [Route("api/ubicaciones/")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiUbicaciones")]
    public class UbicacionesController : ControllerBase
    {


            private IGenericRepository<Ubicacione> repository;
            private IMapper mapper;
            private Response response;


            public UbicacionesController(ApplicationDbContext context, IMapper _mapper)
            {
                this.mapper = _mapper;
                this.repository = new GenericRepository<Ubicacione>(context);
                this.response = new Response();
            }


            /// <summary>
            ///Ubicaciones Get
            /// </summary>
            /// <returns>lista de Ubicaciones</returns>
            [HttpGet("get")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult Get()
            {

                var list = repository.GetAll();

                var listDto = new List<UbicacionesDto>();

                foreach (var row in list)
                {
                    listDto.Add(mapper.Map<UbicacionesDto>(row));
                }

                return Ok(response.ResponseValues(this.Response.StatusCode, listDto));

            }



      
            /// <summary>
            /// Agregar una ubicación
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPost("Add")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Add([FromBody] UbicacionesAddDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }


                if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El registro Ya Existe!!"));
                }

                var ubicacion = mapper.Map<Ubicacione>(dto);
                Convert.ToDateTime("1900-01-01");
                ubicacion.UsuarioIdModInt = 0;

            if (!repository.Add(ubicacion))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal guardar el registro: {dto.DescripcionVar}"));
                }

                return Ok(
                             response.ResponseValues(this.Response.StatusCode,
                                                     mapper.Map<UbicacionesAddDto>(repository.GetById(ubicacion.UbicacionIdVar))
                                                   )
                          );
            }



            /// <summary>
            /// Actualizar ubicación
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPut("Update")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Update([FromBody] UbicacionesUpdateDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }

                if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar && x.UbicacionIdVar != dto.UbicacionIdVar))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Registro Ya Existe!!"));
                }

                var ubicacion = mapper.Map<Ubicacione>(dto);
                var update = repository.GetByValues(x => x.UbicacionIdVar == dto.UbicacionIdVar).FirstOrDefault();
                ubicacion.FechaAltaDate = update.FechaAltaDate;
                ubicacion.UsuarioIdInt = update.UsuarioIdInt;

                if (!repository.Update(ubicacion, ubicacion.UbicacionIdVar))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al actualizar el registro: {dto.DescripcionVar}"));
                }


                return Ok(
                           response.ResponseValues(this.Response.StatusCode,
                                                   mapper.Map<UbicacionesUpdateDto>(repository.GetById(ubicacion.UbicacionIdVar))
                                                 )
                        );

            }



            /// <summary>
            /// Eliminar ubicación por Id
            /// </summary>
            /// <param name="Id"></param>
            /// <returns>StatusCode 200</returns>
            [HttpDelete("Delete/{Id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Delete(object Id)
            {
                if (Id != null)
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El parámetro (Id) es obligatorio"));
                }


                if (repository.Exist(x => x.UbicacionIdVar == Id.ToString()))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El registro con Id: {Id} No existe"));
                }

                var row = repository.GetById(Id);

                var ubicacion = mapper.Map<Role>(row);

                if (!repository.Delete(ubicacion))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al eliminar el registro: {ubicacion.DescripcionVar}"));

                }


                return Ok(response.ResponseValues(this.Response.StatusCode));
            }

        }
    }
