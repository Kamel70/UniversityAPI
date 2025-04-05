using System.ComponentModel.DataAnnotations;
using UniversityAPI.Filters;

namespace UniversityAPI.DTO
{
    public class DeptNameAndLocDTO
    {
        public string Name { get; set; }
        [Required]
        [ValidateLocation("USA", "EG")]
        public string Location { get; set; }
    }
}
