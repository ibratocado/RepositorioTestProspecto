using System.ComponentModel.DataAnnotations;

namespace APIProspecto.Utilities
{
    public class ValidatorName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? objec, ValidationContext validationContext)
        {
            if (objec == null)
                return new ValidationResult("Campo vacio");

            var cadena = objec.ToString();
            if (cadena.Length < 3 || cadena[0] < 'A' || cadena[0] > 'Z')
            {
                return new ValidationResult("Formato no valido");
            }

            return ValidationResult.Success;
        }
    }
}
