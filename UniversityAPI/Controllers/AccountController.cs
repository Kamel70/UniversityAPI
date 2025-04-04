using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.DTO;
using UniversityAPI.Models;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<ApplicationUser> userManager;
        public AccountController(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }
        [HttpPost("Register")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = register.Email;
                user.UserName = register.UserName;
                user.PhoneNumber = register.Phone;
                user.Image = register.Image;
                user.Age = register.Age;
                user.Address = register.Address;
                IdentityResult result =await userManager.CreateAsync(user,register.Password);
                if (result.Succeeded)
                {
                    return Ok("Registered Successfully");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
