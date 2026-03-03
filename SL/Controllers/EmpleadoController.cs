using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        private readonly Empresa _blEmpresa;

        // Constructor con Inyección de Dependencia
        public EmpleadoController(Empresa blEmpresa)
        {
            _blEmpresa = blEmpresa;
        }

        // ---------------------------------------------GetAll

        [HttpGet("GetAllEmpleado")]
        public IActionResult GetAllEmpleado()
        {
            var result = _blEmpresa.GetAllEmpleado();

            if (result.Correct)
                return Ok(result);

            return BadRequest(result);
        }

        // ---------------------------------------------GetById

        [HttpGet("GetByIdEmpleado/{id}")]
        public IActionResult GetByIdEmpleado(int id)
        {
            var result = _blEmpresa.GetByIdEmpleado(id);

            if (result.Correct)
                return Ok(result);

            return NotFound(result);
        }

        // ---------------------------------------------Add
        [HttpPost("AddEmpleado")]
        public IActionResult AddEmpleado([FromBody] ML.Empleado empleado)
        {
            var result = _blEmpresa.AddEmpleado(empleado);

            if (result.Correct)
                return Ok(result);

            return BadRequest(result);
        }

        // ---------------------------------------------Update
        [HttpPut("UpdateEmpleado")]
        public IActionResult UpdateEmpleado([FromBody] ML.Empleado empleado)
        {
            if (empleado == null)
                return BadRequest("Empleado es null");

            var result = _blEmpresa.UpdateEmpleado(empleado);

            if (result.Correct)
                return Ok(result);

            return BadRequest(result);
        }

        // ---------------------------------------------Delete
        [HttpDelete("DeleteEmpleado/{id}")]
        public IActionResult DeleteEmpleado(int id)
        {
            var result = _blEmpresa.DeleteEmpleado(id);

            if (result.Correct)
                return Ok(result);

            return BadRequest(result);
        }
    }
}

