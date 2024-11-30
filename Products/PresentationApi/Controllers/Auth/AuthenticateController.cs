
using AutoMapper;
using Azure;
using Azure.Core;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PresentationApi.Auth.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Resources;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using WebApi.Auth;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Response = WebApi.Auth.Response;

namespace WebApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;




        public AuthenticateController(IMediator mediator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mediator = mediator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("register-superadmin")]
        //[Authorize(Roles = UserRoles.SuperAdmin)] // Requires SuperAdmin role
        public async Task<IActionResult> RegisterSuperAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                // Set other properties based on the model, like FullName
            };
            var passwordValidator = new PasswordValidator<ApplicationUser>();

            // Configure the password requirements
           

            // Check if the password meets the criteria
            var result = await passwordValidator.ValidateAsync(_userManager, null, model.Password);

            if (!result.Succeeded)
            {
                string language = "en";
                if (HttpContext.Request.Headers.TryGetValue("lang", out var values))
                {
                    language = string.IsNullOrEmpty(values.FirstOrDefault()) ? "en" : values.FirstOrDefault();
                }
                ResourceManager resourceManager;
                if (language == "en")
                     resourceManager = new ResourceManager(@"PresentationApi.Resources.ValidationsErrors.ValidationErros_en", typeof(Program).Assembly);
                else 
                     resourceManager = new ResourceManager(@"PresentationApi.Resources.ValidationsErrors.ValidationErrors_ar", typeof(Program).Assembly);

                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = resourceManager.GetString("PasswordValidationMsg") });

            }
             result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));


            if (await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.SuperAdmin);
            }


            return Ok(new Response { Status = "Success", Message = "Super admin created successfully!" });
        }




        [HttpPost]
        [Route("login")]
        public async Task<LoginUserResponse> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                string roles = "";
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    roles += userRole;
                    roles += ",";
                }
                roles = roles.TrimEnd(',');
                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                await _userManager.UpdateAsync(user);
                LoginUserResponse loginUserResponse;
         
                    loginUserResponse = new LoginUserResponse();
               
         
                loginUserResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
                loginUserResponse.RefreshToken = refreshToken;
                loginUserResponse.Expiration = token.ValidTo;
                loginUserResponse.Status = "Success";
                loginUserResponse.Role = roles;

                return loginUserResponse;
            }
            var Res = new LoginUserResponse();
            Res.Status = "Failure";

            return Res;
        }
  [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel model , CancellationToken cancellationToken)
        {
            string language = "en";
            ResourceManager resourceManager;
            if (HttpContext.Request.Headers.TryGetValue("lang", out var values))
            {
                language = string.IsNullOrEmpty(values.FirstOrDefault()) ? "en" : values.FirstOrDefault();
            }

            if (language == "en")
                resourceManager = new ResourceManager(@"PresentationApi.Resources.ValidationsErrors.ValidationErros_en", typeof(Program).Assembly);
            else
                resourceManager = new ResourceManager(@"PresentationApi.Resources.ValidationsErrors.ValidationErrors_ar", typeof(Program).Assembly);

            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return Ok(userExists);
            }

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            // Check if the password meets the criteria
            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var result = await passwordValidator.ValidateAsync(_userManager, null, model.Password);

        
       

         
            result = await _userManager.CreateAsync(user, model.Password);
       
            return Ok(result);
        }

        [HttpPost]
        [Route("register-admin")]
        [Authorize(Roles = UserRoles.SuperAdmin)] 

        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        [HttpPost]
        [Route("Register-Merchant")]
        [Authorize(Roles = UserRoles.SuperAdmin)]

      
       
        [HttpPost]
        [Route("refresh-token")]
        public async Task<TokenModelDto> RefreshToken(TokenModel tokenModel)
        {
            TokenModelDto tokenModel1;

            if (tokenModel is null)
            {
                tokenModel1 = new TokenModelDto();
                tokenModel1.errorCode = -1;
                tokenModel1.errorMSG = "Invalid client request";
                return tokenModel1;
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                tokenModel1 = new TokenModelDto();
                tokenModel1.errorCode = -1;
                tokenModel1.errorMSG = "\"Invalid access token or refresh token\"";
                return tokenModel1;

            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string username = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            var user = await _userManager.FindByNameAsync(username);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                //return BadRequest("Invalid access token or refresh token");

                tokenModel1 = new TokenModelDto()
                {
                    AccessToken = "",
                    RefreshToken = "",
                    errorMSG = "Invalid access token or refresh token",
                    errorCode = -2
            };
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);
             tokenModel1 = new TokenModelDto()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                 errorCode = 0
             };

            return tokenModel1;
            ;
            //return new ObjectResult(new
            //{
            //    accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            //    refreshToken = newRefreshToken
            //});
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return NoContent();
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int RefreshTokenValidityInDays);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(RefreshTokenValidityInDays),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
       
    }
}
