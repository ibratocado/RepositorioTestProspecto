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
    [SwaggerTag("End Points Para Manejo de Documentos")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsService _documentsService;
        private readonly IGenericRespon _genericRespon;

        public DocumentsController(IDocumentsService documentsService,IGenericRespon genericRespon)
        {
            _documentsService = documentsService;
            _genericRespon = genericRespon;
        }

        // GET: api/<DocumentsController>
        [SwaggerOperation(Summary = "Consulta de Documetos por Prospecto",
            Description = "Trae la lista de comentos asiciados a un prospecto")]
        [HttpGet]
        public async Task<ActionResult<GenericRespon>> GetByProspecto(string id)
        {
            try
            {
                var list = await _documentsService.GetByProspecto(id);
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
