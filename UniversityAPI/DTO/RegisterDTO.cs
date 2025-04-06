using System.ComponentModel.DataAnnotations;
using Azure.Identity;
using UniversityAPI.Filters;

namespace UniversityAPI.DTO
{
    public class RegisterDTO
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm Password Must Match Password")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirmed Password")]
        public string ConfirmedPassword { get; set; }
        [Required]
        public int Age {  get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Image {  get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [ValidateRole("Admin", "HR")]
        public string role { get; set; }

    }
}
