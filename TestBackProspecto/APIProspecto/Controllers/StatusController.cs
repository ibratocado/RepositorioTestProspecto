using APIProspecto.DTO;
using APIProspecto.DTO.Interfaces;
using APIProspecto.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIProspecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        private readonly IGenericRespon _genericRespon;

        public StatusController(IStatusService statusService, IGenericRespon genericRespon)
        {
            _statusService = statusService;
            _genericRespon = genericRespon;
        }


        // GET: api/<StatusController>
        [HttpGet("GetFull")]
        public async Task<ActionResult<GenericRespon>> GetFull()
        {
            try
            {
                var list = await _statusService.GetFull();
                var status = StatusCodes.Status200OK;
                if (list.Count <= 0)
                    return StatusCode(status, new { respon = await _genericRespon.SuccesFull(status, "No se encotraron resultados", list) });

                return StatusCode(status, new { respon = await _genericRespon.SuccesFull(status, "Consultado Con Exito", list) });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        
    }
}
