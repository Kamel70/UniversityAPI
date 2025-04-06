using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAPI.DTO
{
    public class StudentDetailsAndUserDetailsDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password Must Match Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name="Birth Of Date")]
        public string BOD { get; set; }
        [Required]
        [Display(Name="Department ID")]
        public int DeptID { get; set; }

    }
}
