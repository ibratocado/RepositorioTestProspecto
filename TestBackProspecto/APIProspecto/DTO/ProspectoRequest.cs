using APIProspecto.Utilities;
using System.ComponentModel.DataAnnotations;

namespace APIProspecto.DTO
{
    public class ProspectoRequest
    {
        [Required]
        [ValidatorName]
        public string? Nombre { get; set; }
        
        [Required]
        [ValidatorName]
        public string? PrimerApellido { get; set; }
        
        [Required]
        [ValidatorName]
        public string? SegundoApellido { get; set; }
        
        [Required]
        public string? Calle { get; set; }
        
        [Required]
        [Range(1,10000,ErrorMessage = "Rango no valido")]
        public int Numero { get; set; }
        
        [Required]
        public string? Colonia { get; set; }

        [Required]
        [MinLength(5,ErrorMessage = "Codigo no Valido")]
        [MaxLength(5,ErrorMessage = "Codigo no Valido")]
        public string? CodigoPostal { get; set; }
        
        [Required]
        [ValidatorPhoneNumber]
        public string? Telefono { get; set; }
        
        [Required]
        [ValidatorRFC]
        public string? RFC { get; set; }
    }
}
