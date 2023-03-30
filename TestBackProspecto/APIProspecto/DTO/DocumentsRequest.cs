using APIProspecto.Utilities;
using System.ComponentModel.DataAnnotations;

namespace APIProspecto.DTO
{
    public class DocumentsRequest
    {
        [Required]
        public List<IFormFile> DocumentData { get; set; }

        [Required]
        [ValidatorGuid]
        public string? ProspectoId { get; set; }

        public DocumentsRequest()
        {
            DocumentData = new List<IFormFile>();
        }
    }
}
