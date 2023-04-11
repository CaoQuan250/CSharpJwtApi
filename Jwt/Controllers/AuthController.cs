using Jwt.Models;
using Jwt.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Jwt.Controllers
{
    public class AuthController : RootController
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private readonly IConfiguration config;
        private readonly JwtcsharpContext context;
        public AuthController(IConfiguration _config,JwtcsharpContext _context)
        {
            context = _context;
            config= _config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterRequest newUser)
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            string saltString = Convert.ToBase64String(salt);
            string password = HashPasword(newUser.Username,newUser.Password,saltString);
            var data = new Customer
            {
                Name= newUser.Name,
                Password=password,
                Age=newUser.Age,
                Salt=saltString,
                Gender=newUser.Gender,
                Address=newUser.Address,
                Username=newUser.Username
            };

            context.Customers.Add(data);
            context.SaveChanges();

            return Ok(newUser);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            var data = context.Customers.FirstOrDefault(r => r.Username == request.Username);
            if (data == null) return BadRequest("Username incorrect");
            string hashPass = HashPasword(data.Username, request.Password, data.Salt);
            bool check = checkValid(data.Username, request.Password, data.Salt, hashPass);
            if (check == false)
            {
                return BadRequest("Password incorrect");
            }
            string token = CreateToken(request);
            return Ok(token);
        }

        private string CreateToken(LoginRequest cusToken)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, cusToken.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                config.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key,
                SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private string HashPasword(string username, string password, string salt)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes($"{username} - {salt} - {password}"));
            foreach (var theByte in crypto) hash.Append(theByte.ToString("x2"));
            return hash.ToString();
        }

        private bool checkValid(string username, string password, string salt, string hashPass)
        {
            if( HashPasword(username, password, salt).Equals(hashPass))
            {
                return true;
            } else
            {
                return false;
            }
            
        }

    }
}
