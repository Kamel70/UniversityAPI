using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UniversityAPI.DTO;
using UniversityAPI.Models;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<ApplicationUser> userManager;
        IConfiguration configuration;
        public AccountController(UserManager<ApplicationUser> _userManager, IConfiguration configuration)
        {
            userManager = _userManager;
            this.configuration = configuration;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user=await userManager.FindByNameAsync(login.Name);
                if (user != null)
                {
                    bool passCheck=await userManager.CheckPasswordAsync(user,login.Password);
                    if (passCheck)
                    {
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                        }
                        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT : SecurityKey"]));
                        SigningCredentials signingCredentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken jwt = new JwtSecurityToken(
                            issuer: configuration["JWT : IssuerIP"],
                            audience: configuration["JWT : AudienceIP"],
                            expires:DateTime.Now.AddDays(1),
                            claims:claims,
                            signingCredentials:signingCredentials
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(jwt),
                            expires = DateTime.Now.AddDays(1)
                        });

                    }
                }
            }
            ModelState.AddModelError("", "Name or Password are invalid");
            return Ok();
        }

        [HttpPost("Register")]
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
