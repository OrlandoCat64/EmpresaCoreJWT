using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BL.Login _BLlogin;
        public LoginController(BL.Login bllogin)
        {
            _BLlogin = bllogin;
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public IActionResult Login([FromBody] ML.Login login)
        {
            ML.Result result = _BLlogin.LoggIn(login);

            if (result.Correct)
            {
                //GENERAR EL TOKEN
                ML.Usuario usuario = (ML.Usuario)result.Object;
                string token = GenerateJwtToken(usuario);

                return Ok(token);
            }
            else
            {
                return BadRequest(result);
            }
        }


        private string GenerateJwtToken(ML.Usuario usuario)
        {
            //Nombre Completo, Rol
            var claims = new[]
            {
            new Claim(ClaimTypes.Role, usuario.RolNombre),
            new Claim(ClaimTypes.Name, usuario.UsuarioNombre),
            new Claim("ApellidoPaterno", usuario.ApellidoPaterno),
            new Claim("ApellidoMaterno", usuario.ApellidoMaterno)

        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jtyfgcb3fjgndkljfngk6578909745ftgdvf"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
