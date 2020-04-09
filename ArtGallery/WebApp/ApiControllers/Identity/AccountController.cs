using System.Threading.Tasks;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp.ApiControllers.Identity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        
        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager, ILogger<AccountController> logger, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {model.Email} not found!");
                return StatusCode(403);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); //get the User analog
                var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims, 
                                            _configuration["JWT:SigningKey"], 
                                            _configuration["JWT:Issuer"], 
                                            _configuration.GetValue<int>("JWT:ExpirationInDays"));
                _logger.LogInformation($"Token generated for user {model.Email}");
                return Ok(new {token = jwt, status="Logged in"});
            }
            
            _logger.LogInformation($"Web-Api login. User {model.Email} attempted to log-in with bad password!");
            return StatusCode(403);
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            var user = new AppUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return StatusCode(200);
            }
            
            _logger.LogInformation("Web-Api register. Cannot create a new account!");
            return StatusCode(403);
        }

        public class LoginDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
            
        }

        public class RegisterDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}