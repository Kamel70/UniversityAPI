using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Filters
{
    public class ValidateLocationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedLocations;

        public ValidateLocationAttribute(params string[] allowedLocations)
        {
            _allowedLocations = allowedLocations;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Location is required.");

            string location = value.ToString().ToLowerInvariant();

            if (_allowedLocations.Any(l => l.ToLowerInvariant() == location))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Invalid location. Allowed locations are: {string.Join(", ", _allowedLocations)}");
        }
    }
}
