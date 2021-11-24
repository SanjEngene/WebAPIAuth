using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPIAuth.Models;
using WebAPIAuth.Services.AuthServices;

namespace WebAPIAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ApplicationContext _context;
        public AccountController(ApplicationContext context)
        {
            context = _context;
        }

        [AllowAnonymous]
        [HttpPost("/token")]
        public IActionResult Login(User user)
        {
            ClaimsIdentity identity = getIdentity(user.Name, user.Password);
            if (identity == null)
            {
                return BadRequest("There is not user with this combination of login and password");
            }

            DateTime now = DateTime.Now;
            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer:AuthOptions.ISSUER,
                    audience:AuthOptions.AUDIENCE,
                    notBefore:now,
                    claims:identity.Claims,
                    expires:now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials:new SigningCredentials(AuthOptions.GetSymmetricalKey(), SecurityAlgorithms.HmacSha256)
                );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }
        private ClaimsIdentity getIdentity(string name, string password)
        {
            User user = _context.Users.FirstOrDefault(i => i.Name == name && i.Password == password);
            if (user != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }
    }
}
