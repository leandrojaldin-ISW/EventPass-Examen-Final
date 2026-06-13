using Microsoft.AspNetCore.Mvc;
using tiendaweb_backend.Datos;
using tiendaweb_backend.Negocio;
namespace tiendaweb_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly GestionUsuarios _gestionUsuarios;
    
    //El mesero controller pide llamar al cocinero Gestor
    public UsuariosController(GestionUsuarios gestionUsuarios)
    {
        _gestionUsuarios = gestionUsuarios;
    }

    [HttpPost("registro")]
    public IActionResult RegistrarUsuario([FromBody] Usuario nuevoUsuario)
    {
        try
        {
            bool exito = _gestionUsuarios.Registrar(nuevoUsuario);

            if (exito)
            {
                return Ok(new { mensaje = "Usuario registrado correctamente" });
            }

            return BadRequest(new { mensaje = "No se pudo registrar el usuario" });
        }
        catch (Exception ex)
        {
            //si algo sale mal lo atrapamos en este error
            return StatusCode(500, new { mensaje = "Error interno: " + ex.Message });
        }
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuario credenciales)
    {
        var usuarioEncontrado = _gestionUsuarios.IniciarSesion(credenciales.Correo, credenciales.Password);
        if (usuarioEncontrado == null)
        {
            return Unauthorized(new { mensaje = "Correo o contraseña incorrectos" });
        }

        return Ok(usuarioEncontrado);
    }
}
