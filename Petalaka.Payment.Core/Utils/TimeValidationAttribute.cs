using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

public class TimeValidationAttribute : ValidationAttribute
{
    private readonly string _format;

    public TimeValidationAttribute(string format = @"hh\:mm")
    {
        _format = format;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is TimeSpan timeSpanValue)
        {
            // Convert TimeSpan to a string and check if it matches the required format
            string formattedTime = timeSpanValue.ToString(_format);
            if (TimeSpan.TryParseExact(formattedTime, _format, CultureInfo.InvariantCulture, out _))
            {
                return ValidationResult.Success;
            }
        }
        else if (value is string timeString)
        {
            if (TimeSpan.TryParseExact(timeString, _format, CultureInfo.InvariantCulture, out _))
            {
                return ValidationResult.Success;
            }
        }
        return new ValidationResult("Invalid time format. Use HH:mm.");
    }
}
