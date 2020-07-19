using CheuwoAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CheuwoAPI.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UsersController : ApiHandler
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;


        public UsersController(
            IConfiguration config,
            UserManager<User> userManager,
            SignInManager<User> signInManager
            )
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO model)
        {
            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return ApiBadRequest(result.Errors.First().Description);

            return Created("", new
            {
                token = GenerateJWTToken(new UserDTO()
                {
                    Email = user.Email
                })
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return ApiBadRequest("User does not exist.");

            var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, false, lockoutOnFailure: false);
            if (result.IsLockedOut)
                return ApiBadRequest("User account locked out.");

            if (!result.Succeeded)
                return ApiBadRequest("Invalid username or password.");

            return Ok(new
            {
                token = GenerateJWTToken(new UserDTO()
                {
                    Email = user.Email
                })
            });
        }

        string GenerateJWTToken(UserDTO userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var token = new JwtSecurityToken(
            issuer: _config["JWTIssuer"],
            audience: _config["JWTAudience"],
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
