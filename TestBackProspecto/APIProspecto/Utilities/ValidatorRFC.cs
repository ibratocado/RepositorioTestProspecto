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
            
            if(rfc.Length < 18)
                return new ValidationResult("Formato No Valido");

            if (rfc[0] < 'A' || rfc[0] > 'Z')
                return new ValidationResult("Formato No Valido");

            if ((rfc[4] < '0' || rfc[4] > '9'))
                return new ValidationResult("Formato No Valido");

            if (rfc[9] < '0' || rfc[9] > '9')
                return new ValidationResult("Formato No Valido");

            return ValidationResult.Success;
        }
    }
}
