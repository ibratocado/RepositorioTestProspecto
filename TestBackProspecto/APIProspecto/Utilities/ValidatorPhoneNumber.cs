using System.ComponentModel.DataAnnotations;

namespace APIProspecto.Utilities
{
    public class ValidatorPhoneNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? objec, ValidationContext validationContext)
        {
            if (objec == null)
                return new ValidationResult("Campo vacio");

            var phone = objec.ToString();

            foreach (var item in phone)
            {
                if (item < '0' || item > '9' || phone.Length < 10)
                    return new ValidationResult("Formato de Numero no Valido");
            }

            return ValidationResult.Success;
        }
    }
}
