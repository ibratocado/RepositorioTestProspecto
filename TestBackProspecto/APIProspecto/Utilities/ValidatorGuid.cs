using System.ComponentModel.DataAnnotations;

namespace APIProspecto.Utilities
{
    public class ValidatorGuid : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? objec, ValidationContext validationContext)
        {
            //708972db-dd65-4e12-aa2d-6618c534f3dd
            if (objec == null)
                return new ValidationResult("Campo vacio");

            var guid = objec.ToString();

            if (guid.Length != 36 &&
                guid[8] != '-' &&
                guid[13] != '-' &&
                guid[18] != '-' &&
                guid[23] != '-')
                return new ValidationResult("Guid no valido");

            return ValidationResult.Success;
        }
    }
}
