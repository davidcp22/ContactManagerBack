using ContactManagerAPI.Contants;
using ContactManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Operators;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User userLogin)
        {
            if (userLogin == null) { return BadRequest(); }
            var user = Authenticate(userLogin);
            var token = GenerateToken(user);

            if ( user!= null)
            {
                return Ok( token);
            }
            return NotFound("User Not found");
        }

        private User Authenticate(User userLogin)
        {
            var currentUser = UserContants.Users.FirstOrDefault( us => us.Username.ToLower() == userLogin.Username.ToLower() && us.Password == userLogin.Password);

            if (currentUser != null) {
                return currentUser;
            }
            return null;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim("TestClaimPrivate", "TestUNit"),
            };


            // Crear el token

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }

}
