using EAU.Web.NRA.IdentitySimulator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EAU.Web.NRA.IdentitySimulator.Controllers
{
    public class TokenController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new TokenViewModel());
        }

        [HttpPost]
        public IActionResult CreateToken(TokenViewModel model, [FromServices] IConfiguration configuration)
        {
            string signingKey = configuration.GetSection("NRA").GetValue<string>("SigningKey");
            string returnUrl = configuration.GetSection("NRA").GetValue<string>("ReturnUrl");

            byte[] key = Encoding.UTF8.GetBytes(signingKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("iss", "NRA"),
                    new Claim("id", model.UserIdentifier),
                    new Claim("ty", "0")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Redirect($"{returnUrl}?jwt={(token as JwtSecurityToken).RawData}");
        }
    }
}
