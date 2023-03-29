using APIProspecto.Utilities;
using System.ComponentModel.DataAnnotations;

namespace APIProspecto.DTO
{
    public class ProspestoUpdateStatusRespon
    {
        [Required]
        [Range(1,3,ErrorMessage = "No valido")]
        public int Status { get; set; }

        [Required]
        [ValidatorGuid]
        public string? Id { get; set; }
    }
}
