using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Validation
{
    public class Shirt_EnsureCorrectSizingAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var shirt = validationContext.ObjectInstance as Shirt;
            if (shirt != null)
            {
                if (shirt.Gender.Equals("men", StringComparison.OrdinalIgnoreCase) && shirt.Size < 8)
                {
                    return new ValidationResult("For men's, The size has to be greater or equal to 8");
                }
                else if (shirt.Gender.Equals("wowen", StringComparison.OrdinalIgnoreCase) && shirt.Size < 6)
                {
                    return new ValidationResult("For women, The size has to be greater or equal to 6");
                }
            }
            return ValidationResult.Success;
        }
    }
}
