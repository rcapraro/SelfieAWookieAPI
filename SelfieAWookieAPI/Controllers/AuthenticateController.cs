using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookie.Core.Infrastructure.Configuration;
using SelfieAWookieAPI.Application.Dto;

namespace SelfieAWookieAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        #region Constructors

        public AuthenticateController(
            ILogger<AuthenticateController> logger,
            UserManager<IdentityUser> userManager,
            IOptions<SecurityOption> securityOption)
        {
            _logger = logger;
            _userManager = userManager;
            _securityOption = securityOption.Value;
        }

        #endregion

        #region Internal methods

        private string GenerateJwtToken(IdentityUser user)
        {
            // Now its ime to define the jwt token which will be responsible of creating our tokens
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // We get our secret from the app settings
            var jwtKey = Encoding.UTF8.GetBytes(_securityOption.Key);

            // we define our token descriptor
            // We need to utilise claims which are properties in our token which gives information about the token
            // which belong to the specific user who it belongs to
            // so it could contain their id, name, email the good part is that these information
            // are generated by our server and identity framework which is valid and trusted
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    // the JTI is used for our refresh token which we will be covering in the next video
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                // the life span of the token needs to be shorter and utilise refresh token to keep the user signed in
                // but since this is a demo app we can extend it to fit our current need
                Expires = DateTime.UtcNow.AddHours(6),
                // here we are adding the encryption algorithm information which will be used to decrypt our token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        #endregion

        #region Public methods

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserDto userDto)
        {
            _logger.LogInformation("Authenticating user {Login}", userDto.Login);

            IActionResult result = BadRequest();

            var user = await _userManager.FindByEmailAsync(userDto.Login);
            if (user == null) return result;
            if (await _userManager.CheckPasswordAsync(user, userDto.Password))
                result = Ok(new AuthenticateUserDto
                {
                    Login = user.Email,
                    Name = user.UserName,
                    Token = GenerateJwtToken(user)
                });

            return result;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticateUserDto userDto)
        {
            _logger.LogInformation("Registering user {Name}", userDto.Name);
            
            IActionResult result = BadRequest();

            var user = new IdentityUser(userDto.Login)
            {
                Email = userDto.Login,
                UserName = userDto.Name
            };
            var identityResult = await _userManager.CreateAsync(user, userDto.Password);

            if (!identityResult.Succeeded) return result;
            userDto.Token = GenerateJwtToken(user);
            result = Ok(userDto);

            return result;
        }

        #endregion

        #region fields

        private readonly ILogger<AuthenticateController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SecurityOption _securityOption;

        #endregion
    }
}