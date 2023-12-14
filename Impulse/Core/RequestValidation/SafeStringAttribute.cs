using System.ComponentModel.DataAnnotations;

namespace Impulse.Core.RequestValidation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class SafeStringAttribute : ValidationAttribute
    {
        private const int MaxLength = 55500;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success; // Null values are allowed.
            }

            if (value is string strValue)
            {
                // Check for SQL injection and XSS vulnerabilities.
                if (strValue.Contains("sql", StringComparison.OrdinalIgnoreCase) ||
                    strValue.Contains("xss", StringComparison.OrdinalIgnoreCase) ||
                    strValue.Contains("command", StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult("Property contains prohibited content.");
                }

                // Check for length.
                if (strValue.Length > MaxLength)
                {
                    return new ValidationResult($"Property length should not exceed {MaxLength} characters.");
                }

                // Check for non-word characters.
                if (!System.Text.RegularExpressions.Regex.IsMatch(strValue, @"^\w+$"))
                {
                    return new ValidationResult("Property should contain only word characters.");
                }

                return ValidationResult.Success; // Property meets the criteria.
            }

            return new ValidationResult("Property must be a string.");
        }
    }
}
