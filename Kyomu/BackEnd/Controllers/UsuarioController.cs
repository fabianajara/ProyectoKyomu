using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<UsuarioDTO> Get()
        {
            return _usuarioService.GetUsuarios();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public UsuarioDTO Get(int id)
        {
            return _usuarioService.GetUsuarioById(id);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public void Post([FromBody]UsuarioDTO usuario)
        {
            _usuarioService.Add(usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public ActionResult<UsuarioDTO> Put( [FromBody] UsuarioDTO usuario)
        {
            _usuarioService.Update(usuario);
            return Ok(usuario);
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _usuarioService.Delete(id);
        }

        // GET api/Usuario/Login?correo=...&contraseña=...
        [HttpGet("Login")]
        public ActionResult<UsuarioDTO> Login(string correo, string contraseña)
        {
            var usuario = _usuarioService.Login(correo, contraseña);
            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas");
            }
            return Ok(usuario);
        }

        // Cambiar a POST para mayor seguridad
        [HttpPost("Login")]
        public ActionResult<UsuarioDTO> Login([FromBody] LoginDTO loginDTO)
        {
            // Validar los parámetros (correo y contraseña)
            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.CorreoElectronico) || string.IsNullOrEmpty(loginDTO.Contraseña))
            {
                return BadRequest("Correo o contraseña no válidos.");
            }

            // Llamada al servicio para verificar las credenciales
            var usuario = _usuarioService.Login(loginDTO.CorreoElectronico, loginDTO.Contraseña);

            // Si no se encuentra el usuario, retornar Unauthorized
            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas");
            }

            // Si el login es exitoso, devolver el usuario
            return Ok(usuario);
        }

    }
}
