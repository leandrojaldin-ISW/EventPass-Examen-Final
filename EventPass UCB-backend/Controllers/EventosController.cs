using Microsoft.AspNetCore.Mvc;
using tiendaweb_backend.Datos;
using tiendaweb_backend.Negocio;

namespace tiendaweb_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class EventosController : ControllerBase
{
    private readonly GestionEventos _gestionEventos;

    //Inyeccion de dependecias
    public EventosController(GestionEventos gestionEventos)
    {
        _gestionEventos = gestionEventos;
    }

    [HttpGet("lista")]
    public IActionResult ListaEventos()
    {
        return Ok(_gestionEventos.ListaEventos());
    }

    [HttpGet("buscar/{id}")]
    public IActionResult BuscarEvento(int id)
    {
        var evento = _gestionEventos.BusccarPorId(id);
        if (evento == null)
        {
            return NotFound(new { mensaje = "Evento no encontrado" });
        }

        return Ok(evento);
    }

    [HttpPost("crear")]
    public IActionResult Crear([FromBody] Evento nuevoEvento)
    {
        var eventoCreado = _gestionEventos.CrearEvento(nuevoEvento);
        return Ok(eventoCreado);
    }

    [HttpPut("editar")]
    public IActionResult Actualizar(int id, [FromBody] Evento eventoEditado)
    {
        var actualizado = _gestionEventos.ActualizarEvento(id, eventoEditado);
        if (actualizado == null)
        {
            return NotFound(new { mensaje = "Evento no encontrado" });
        }

        return Ok(actualizado);
    }

    [HttpDelete("borrar/{id}")]
    public IActionResult Borrar(int id)
    {
        bool exito = _gestionEventos.EliminarEvento(id);
        if (exito)
        {
            return Ok (new {mensaje = "Eliminado correctamente"});
        }

        return BadRequest(new { mensaje = "No se pudo eliminar el evento" });
    }
    
}