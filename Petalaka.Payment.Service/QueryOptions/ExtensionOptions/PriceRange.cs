using System.ComponentModel.DataAnnotations;

namespace Petalaka.Payment.Service.QueryOptions.ExtensionOptions;

public class PriceRange : IValidatableObject
{
    public decimal? PriceFrom { get; set; } = 0;
    // Default value for PriceTo is 10,000,000
    public decimal? PriceTo { get; set; } = 10000000;
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (PriceFrom > PriceTo)
        {
            yield return new ValidationResult("'PriceFrom' cannot be greater than 'PriceTo'.");
        }
    }
}