using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Filters
{
    public class ValidateRoleAttribute:ValidationAttribute
    {
        private readonly string[] _allowedRoles;

        public ValidateRoleAttribute(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Role is required.");

            string role = value.ToString().ToLowerInvariant();

            if (_allowedRoles.Any(l => l.ToLowerInvariant() == role))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Invalid Role. Allowed Roles are: {string.Join(", ", _allowedRoles)}");
        }
    }
}
