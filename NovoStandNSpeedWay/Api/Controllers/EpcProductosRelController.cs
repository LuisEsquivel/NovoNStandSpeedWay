using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.EpcProductosRel;
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
        /// EpcProductosRel Controller
        /// </summary>
        [Route("api/epc/")]
        [ApiController]
        [ApiExplorerSettings(GroupName = "ApiEpcProductosRel")]
   
    public class EpcProductosRelController : ControllerBase
    {

            private IGenericRepository<EpcProductosRel> repository;
            private IMapper mapper;
            private Response response;


            public EpcProductosRelController(ApplicationDbContext context, IMapper _mapper)
            {
                this.mapper = _mapper;
                this.repository = new GenericRepository<EpcProductosRel>(context);
                this.response = new Response();
            }


            /// <summary>
            ///EpcProductosRel Get
            /// </summary>
            /// <returns>lista de epc productos rel</returns>
            [HttpGet("get")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult Get()
            {

                var list = repository.GetAll();

                var listDto = new List<EpcProductosRelDto>();

                foreach (var row in list)
                {
                    listDto.Add(mapper.Map<EpcProductosRelDto>(row));
                }

                return Ok(response.ResponseValues(this.Response.StatusCode, listDto));

            }



            /// <summary>
            /// Obtener epc por el Id
            /// </summary>
            /// <param name="Id"></param>
            /// <returns>StatusCode 200</returns>
            [HttpGet("GetById/{Id:int}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public IActionResult GetById(int Id)
            {
                var row = this.repository.GetById(Id);
                var listDto = new List<EpcProductosRelDto>();
                listDto.Add(mapper.Map<EpcProductosRelDto>(row));
                return Ok(this.response.ResponseValues(this.Response.StatusCode, listDto));
            }





            /// <summary>
            /// Agregar un nuevo epc producto rel
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPost("Add")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Add([FromBody] EpcProductosRelAddDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }


            if (repository.Exist(x => x.EpcVar == dto.EpcVar && x.ProductoIdVar  == dto.ProductoIdVar))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El registro Ya Existe!!"));
            }

            var o = mapper.Map<EpcProductosRel>(dto);

                if (!repository.Add(o))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal guardar el registro: {dto.EpcVar}"));
                }

                return Ok(
                             response.ResponseValues(this.Response.StatusCode,
                                                     mapper.Map<EpcProductosRelDto>(repository.GetById(o.EpcVar))
                                                   )
                          );
            }



            /// <summary>
            /// Actualizar epc 
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpPut("Update")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Update([FromBody] EpcProductosRelUpdateDto  dto)
            {
                if (dto == null)
                {
                    return BadRequest(StatusCodes.Status406NotAcceptable);
                }

            if (repository.Exist(x => x.EpcVar  == dto.EpcVar && x.ProductoIdVar  != dto.ProductoIdVar))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Registro Ya Existe!!"));
            }

                var o = mapper.Map<EpcProductosRel>(dto);
                var update = repository.GetByValues(x => x.ProductoIdVar  == dto.ProductoIdVar  && x.EpcVar  == dto.EpcVar).FirstOrDefault();


                if (!repository.Update(o, o.ProductoIdVar))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al actualizar el registro: {dto.EpcVar}"));
                }


                return Ok(
                           response.ResponseValues(this.Response.StatusCode,
                                                   mapper.Map<EpcProductosRelDto>(repository.GetById(o.ProductoIdVar))
                                                 )
                        );

            }



            /// <summary>
            /// Eliminar epc por Id
            /// </summary>
            /// <param name="dto"></param>
            /// <returns>StatusCode 200</returns>
            [HttpDelete("Delete/{Id:int}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public IActionResult Delete(EpcProductosRelDto dto)
            {
                if (dto == null)
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El parámetro (Id) es obligatorio"));
                }


                if (repository.Exist(x => x.ProductoIdVar == dto.ProductoIdVar  && x.EpcVar == dto.EpcVar ))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El registro con Id: {dto.EpcVar} No existe"));
                }

                var o = mapper.Map<EpcProductosRel>(dto);

                if (!repository.Delete(o))
                {
                    return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al eliminar el registro: {o.EpcVar}"));

                }


                return Ok(response.ResponseValues(this.Response.StatusCode));
            }

        }
    }
