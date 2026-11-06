using System.ComponentModel.DataAnnotations;

namespace CarDealerShip.Validation
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;//放副檔名

        public AllowedExtensionsAttribute(params string[] extensions)//params 接任意數量參數
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validation)
        {
            //value就是需要被驗證的屬性，因為可能是任何物件所以用object
            //validation驗證實的背景資訊，可以想成是VM


            if(value is not List<IFormFile>)
            {
                return ValidationResult.Success;//本質上是null
            }

            foreach (var f in (List<IFormFile>)value)
            {
                string extension = Path.GetExtension(f.FileName);
                if (!_extensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
