using Azure.Identity;

namespace UniversityAPI.DTO
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public int Age {  get; set; }
        public string Address { get; set; }
        public string Image {  get; set; }
        public string Phone { get; set; }

    }
}
