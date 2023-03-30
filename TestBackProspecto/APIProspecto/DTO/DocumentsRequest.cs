using APIProspecto.Utilities;
using System.ComponentModel.DataAnnotations;

namespace APIProspecto.DTO
{
    public class DocumentsRequest
    {
        [Required]
        public IFormFile? DocumentData { get; set; }

        [Required]
        [ValidatorGuid]
        public string? ProspectoId { get; set; }
    }
}
