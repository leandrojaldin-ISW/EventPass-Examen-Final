using Microsoft.AspNetCore.Mvc;
using tiendaweb_backend.Datos;
using tiendaweb_backend.Negocio;

namespace tiendaweb_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComentariosController : ControllerBase
{
    private readonly GestionComentarios _gestionComentarios;
    
    //Inyectamos el gestor por el contructor
    public ComentariosController(GestionComentarios gestionComentarios)
    {
        _gestionComentarios = gestionComentarios;
    }

    [HttpGet]
    public IActionResult ConsultarTodos()
    {
        return Ok(_gestionComentarios.ObtenerTodos());
    }

    [HttpGet("evento/{idEvento}")]
    public IActionResult ConsultarPorEvento(int idEvento)
    {
        return Ok(_gestionComentarios.ObtenerPorEvento(idEvento));
    }

    [HttpPost]
    public IActionResult Registrar([FromBody] Comentario nuevoComentario)
    {
        var resultado = _gestionComentarios.CrearComentario(nuevoComentario);
        if (resultado == null)
        {
            return BadRequest(new { mensaje = "Datos invalidos o el evento no existe" });
        }

        return Ok(resultado);
    }

    [HttpPut("{id}")]
    public IActionResult Actualizar(int id, [FromBody] string nuevoTexto)
    {
        var resultado = _gestionComentarios.ActualizarComentario(id, nuevoTexto);
        if (resultado == null)
        {
            return BadRequest(new { mensaje = "No se pudo actualizar el comentario" });
        }

        return Ok(resultado);
    }


    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        var exito = _gestionComentarios.EliminarComentario(id);
        if (exito)
        {
            return Ok(new { mensaje = "Comentario eliminado exitosamente" });
        }

        return NotFound(new { mensaje = "Comentario no encontrado" });
    }
    
    
}