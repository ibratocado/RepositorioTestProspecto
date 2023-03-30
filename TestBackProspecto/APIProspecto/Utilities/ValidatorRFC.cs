using System.ComponentModel.DataAnnotations;

namespace APIProspecto.Utilities
{
    public class ValidatorRFC : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? objec, ValidationContext validationContext)
        {
            if (objec == null)
                return new ValidationResult("Campo vacio");

            var rfc = objec.ToString();
            
            if(rfc.Length < 13)
                return new ValidationResult("Menor a 13 caracteres");

            if ((rfc[0] < 'A' || rfc[0] > 'Z') && (rfc[0] < 'a' || rfc[0] > 'z'))
                return new ValidationResult("Formato No Valido letras inicio");

            if ((rfc[4] < '0' || rfc[4] > '9'))
                return new ValidationResult("Formato No Valido nuemros");

            if (rfc[9] < '0' || rfc[9] > '9')
                return new ValidationResult("Formato No Valido");

            return ValidationResult.Success;
        }
    }
}
