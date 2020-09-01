using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApiSegura.Models.BD;

namespace WebApiSegura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly RENTMOVIEContext _context = new RENTMOVIEContext();
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Usuario login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);
            if (user!=null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        private object GenerateJSONWebToken(Usuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            IdentityModelEventSource.ShowPII = true;
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Nombre+" "+userInfo.Apellido),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,userInfo.Rol.ToString()),
                new Claim(ClaimTypes.PrimarySid,userInfo.Idusuario.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires:DateTime.Now.AddMinutes(120),
                signingCredentials:credentials
                );
            //return Ok(new
            //{
            //    token = new JwtSecurityTokenHandler().WriteToken(token),
            //    expira = token.ValidTo
            //});
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Usuario AuthenticateUser(Usuario login)
        {
            var result = _context.Usuario.Where(x => x.Email == login.Email && x.Password == login.Password).First();
            return result;
        }
    }
}
