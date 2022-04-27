using InternExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace InternExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersEmailAuthenticationController : ControllerBase
    {
        public static UserEmailRegister users = new UserEmailRegister();
        private readonly IConfiguration _configuration;

        public UsersEmailAuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserEmailRegister>> Register(UserEmailDto request)
        {
            CreatePasswordHash(request.UsersEmail, out byte[] UsersPasswordHash, out byte[] UsersPasswordSalt);

            users.UsersEmail = request.UsersEmail;
            users.UsersPasswordHash = UsersPasswordHash;
            users.UsersPasswordSalt = UsersPasswordSalt;

            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserEmailDto request)
        {
            if (users.UsersEmail != request.UsersEmail)
            {
                return BadRequest("Email not found.");
            }

            if (!VerifyPasswordHash(request.UsersPassword, users.UsersPasswordHash, users.UsersPasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(users);
            return Ok(token);
        }

        private string CreateToken(UserEmailRegister user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, users.UsersEmail),
                new Claim(ClaimTypes.Role, "user")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
