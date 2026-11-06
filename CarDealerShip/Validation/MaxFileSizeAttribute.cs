using System.ComponentModel.DataAnnotations;

namespace CarDealerShip.Validation
{
    public class MaxFileSizeAttribute :ValidationAttribute
    {
        private readonly long _maxBytes;

        public MaxFileSizeAttribute(long maxBytes)
        {
            _maxBytes = maxBytes;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not List<IFormFile>)
            {
                return ValidationResult.Success;
            }

            foreach (var f in (List<IFormFile>)value)
            {
               
                if (f.Length > _maxBytes)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }

    }
}
