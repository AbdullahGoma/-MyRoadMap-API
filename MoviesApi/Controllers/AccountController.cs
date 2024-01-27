using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> _userManager, IConfiguration _config)
        {
            userManager = _userManager;
            config = _config;
        }
        // Create Account => Post (New User) "Registration"
        [HttpPost("register")] // api/account/register
        public async Task<IActionResult> Registration(RegisterUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                // Save
                ApplicationUser user = new ApplicationUser();
                user.UserName = userDTO.UserName;
                user.Email = userDTO.Email;
                IdentityResult result = await userManager.CreateAsync(user, userDTO.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Added Success");
                }
                foreach (var error in result.Errors)
                {
                    // Return First Only
                    //ModelState.AddModelError("", error.Description);
                    return BadRequest(error.Description);
                }
            }
            return BadRequest(ModelState);
        }
        // Login => Create Jwt token
        // Check If Account(Username, Password) Valid "Login" "Post"
        [HttpPost("Login")] // api/acount/login
        public async Task<IActionResult> Login(LoginUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                // Check ==> Create Token
                ApplicationUser user = await userManager.FindByNameAsync(userDTO.UserName);
                if (user != null) // Username Found
                {
                    bool found = await userManager.CheckPasswordAsync(user, userDTO.Password);
                    if (found)
                    {
                        // Claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        // Get Role
                        var Roles = await userManager.GetRolesAsync(user);
                        foreach (var Role in Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, Role));
                        }

                        SecurityKey securityKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"]));
                        SigningCredentials signing = new SigningCredentials(
                            securityKey,
                            SecurityAlgorithms.HmacSha256
                            );


                        // Create Token
                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"], // Url => Web API
                            audience: config["JWT:ValidAudiance"], // audience on Angular on 4200 (Url Consumer => Angular)
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signing
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                    return Unauthorized();
                }
                return Unauthorized();
            }
            return Unauthorized();
        }
    }
}
