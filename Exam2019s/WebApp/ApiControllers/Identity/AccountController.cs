﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicApi.DTO;
using PublicApi.DTO.Identity;
using AppUser = Domain.Identity.AppUser;

namespace WebApp.ApiControllers.Identity
{
    /// <summary>
    /// Api endpoint for registering new user and user log-in (jwt token generation)
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="logger"></param>
        /// <param name="bll"></param>
        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, ILogger<AccountController> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint for user log-in (jwt generation)
        /// </summary>
        /// <param name="dto">login data</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"WebApi login. User {dto.Email} not found!");
                return NotFound(new MessageDTO("User not found!"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = IdentityExtensions.GenerateJWT(
                    claimsPrincipal.Claims
                        .Append(new Claim(ClaimTypes.GivenName, appUser.UserName)),
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"WebApi login. User {appUser.Email} logged in.");
                return Ok(new JwtResponseDTO()
                {
                    Token = jwt, Status = $"User {appUser.Email} logged in.", UserName = appUser.UserName,
                    UserRoles = await _userManager.GetRolesAsync(appUser), AppUserId = appUser.Id
                });
            }
            _logger.LogInformation($"WebApi login. User {appUser.Email} failed login attempt!");
            return NotFound(new MessageDTO("User not found!"));
        }

        /// <summary>
        /// Endpoint for user registration and immediate log-in (jwt generation) 
        /// </summary>
        /// <param name="dto">user data</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser != null)
            {
                _logger.LogInformation($"WebApi register. User {dto.Email} already registered!");
                return NotFound(new MessageDTO("User already registered!"));
            }

            appUser = new AppUser()
            {
                Email = dto.Email,
                UserName = dto.Email,
            };
            var result = await _userManager.CreateAsync(appUser, dto.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {appUser.Email} created a new account with password.");
                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var jwt = IdentityExtensions.GenerateJWT(
                        claimsPrincipal.Claims
                            .Append(new Claim(ClaimTypes.GivenName, appUser.UserName)),
                        _configuration["JWT:SigningKey"],
                        _configuration["JWT:Issuer"],
                        _configuration.GetValue<int>("JWT:ExpirationInDays")
                    );
                    _logger.LogInformation($"WebApi register. User {user.Email} logged in.");
                    return Ok(new JwtResponseDTO()
                    {
                        Token = jwt, Status = $"User {user.Email} created and logged in.", UserName = user.UserName,
                        UserRoles = await _userManager.GetRolesAsync(user), AppUserId = user.Id
                    });
                }

                _logger.LogInformation($"User {appUser.Email} not found after creation!");
                return BadRequest(new MessageDTO("User not found after creation!"));
            }

            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new MessageDTO() {Messages = errors});
        }
    }
}