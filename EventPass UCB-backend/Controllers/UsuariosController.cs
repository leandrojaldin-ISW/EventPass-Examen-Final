using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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
    public IActionResult Login([FromBody] JsonElement datosLogin)
    {
        try
        {
            // 1. Extraemos los datos que nos manda Angular
            string correo = datosLogin.GetProperty("correo").GetString();
            string password = datosLogin.GetProperty("password").GetString();

            // 2. Le pedimos a la capa de Negocio que verifique en la Base de Datos
            var usuario = _gestionUsuarios.AutenticarUsuario(correo, password);

            // 3. Validamos el resultado
            if (usuario == null)
            {
                // Si no existe o la contraseña está mal, devolvemos un error 401 (No autorizado)
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });
            }

            // 4. Si todo sale bien, devolvemos los datos clave del usuario
            // OJO: Nunca devolvemos la contraseña por seguridad
            return Ok(new 
            { 
                id = usuario.Id, 
                nombre = usuario.Nombre,
                correo = usuario.Correo
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = $"Error al procesar el login: {ex.Message}" });
        }
    }
}
