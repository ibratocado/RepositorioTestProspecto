using APIProspecto.DTO;
using APIProspecto.DTO.Interfaces;
using APIProspecto.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIProspecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("End Points para Manejo de Prospectos")]
    public class ProspectoController : ControllerBase
    {
        private readonly IGenericRespon _genericRespon;
        private readonly IProspectoService _prospectoService;

        public ProspectoController(IGenericRespon genericRespon, IProspectoService prospectoService)
        {
            _genericRespon = genericRespon;
            _prospectoService = prospectoService;
        }

        [SwaggerOperation(Summary = "Consulta de Prospectos", 
            Description = "Trae un listado de prospectos por status")]
        [HttpGet("GetStateFull/{state}")]
        public async Task<ActionResult<IEnumerable<GenericRespon>>> GetStateFull(int state)
        {
            if (state<0)
                return BadRequest();
            try
            {
                var result = await _prospectoService.GetStateFull(state);
                var status = StatusCodes.Status200OK;
                if (result.Count == 0)
                    return StatusCode(status,new { respon = await _genericRespon.SuccesFull(status, "No se encotraron resultados", result) });

                return StatusCode(status, new { respon = await _genericRespon.SuccesFull(status, "Consultado Con Exito", result) });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [SwaggerOperation(Summary = "Consulta de Prospecto por Id",
            Description = "Trae un Prospecto dependiendo su 'Id'")]
        [HttpGet("GetOne/{id}")]
        public async Task<ActionResult<object>> GetOne(string id)
        {
            try
            {
                var result = await _prospectoService.GetById(id);
                var status = StatusCodes.Status200OK;
                if (result.Id == null)
                    return StatusCode(status, new { respon = await _genericRespon.SuccesFull(status, "No se encotraron resultados", result) });

                return StatusCode(status, new { respon = await _genericRespon.SuccesFull(status, "Consultado Con Exito", result) });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [SwaggerOperation(Summary = "Insercion de Prospecto",
            Description = "Crea un prospecto con la data enviada en status 'Enviado'")]
        [HttpPost]
        public async Task<ActionResult<GenericRespon>> Post([FromBody] ProspectoRequest value)
        {
            try
            {
                var exist = await _prospectoService.GetExistRFC(value.RFC);
                if (exist)
                    return StatusCode(StatusCodes.Status406NotAcceptable,
                       await _genericRespon.Error(StatusCodes.Status406NotAcceptable, "Ya esxite un Prospecto con los mismos datos"));

                var id = await _prospectoService.InsertProspecto(value);
                var state = StatusCodes.Status200OK;
                var respon = await _genericRespon.SuccesFull(state, "Agregado Con Exito", id);
                return StatusCode(state, new { respon = respon });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                                       await _genericRespon.Error(StatusCodes.Status406NotAcceptable, "A ocurrido un error"));
            }
        }



        [SwaggerOperation(Summary = "Actualizar el estado de la solicitud",
            Description = "Cambia el status de la solicitud del prospecto")]
        [HttpPut("UpdateStatus")]
        public async Task<ActionResult<GenericRespon>> UpdateStatus([FromBody] ProspestoUpdateStatusRespon request)
        {
            try
            {
                _prospectoService.UpdateStateByProspeto(request.Id, request.Status);
                return StatusCode(StatusCodes.Status200OK, new { respon = await _genericRespon.SuccesFull(200, "Actualizado con Exito",null) });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                                       await _genericRespon.Error(StatusCodes.Status406NotAcceptable, "A ocurrido un error"));
            }
        }

    }
}
