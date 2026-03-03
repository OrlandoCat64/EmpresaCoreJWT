using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public IActionResult GetAll()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity; //leyendo el token
            if (identity != null)
            {
                //IEnumerable<Claim> claims = identity.Claims;
                // or
                //var name = identity.FindFirst("Name").Value;
                var apellidoPaterno = identity.FindFirst("ApellidoPaterno").Value;
                var apellidoMaterno = identity.FindFirst("ApellidoMaterno").Value;

                var objeto = new
                {
                    //name,
                    apellidoPaterno,
                    apellidoMaterno
                };

                return Ok(objeto);
            }
            return Ok("Lista de usuarios");
        }
    }
}
