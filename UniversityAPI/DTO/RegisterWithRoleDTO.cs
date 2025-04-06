using System.ComponentModel.DataAnnotations;
using UniversityAPI.Filters;

namespace UniversityAPI.DTO
{
    public class RegisterWithRoleDTO
    {
        public RegisterDTO Register { get; set; }
        [Required]
        [ValidateRole("Admin","HR")]
        public string role {  get; set; }
    }
}
