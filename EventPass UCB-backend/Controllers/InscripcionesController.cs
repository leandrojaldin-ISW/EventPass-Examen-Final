using Microsoft.AspNetCore.Mvc;
using tiendaweb_backend.Datos;
using tiendaweb_backend.Negocio;

namespace tiendaweb_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InscripcionesController : ControllerBase
{
    private readonly GestionInscripciones _gestionInscripciones;

    public InscripcionesController(GestionInscripciones gestionInscripciones)
    {
        _gestionInscripciones = gestionInscripciones;
    }

    [HttpPost]
    public IActionResult Inscribir([FromBody] Inscripcion nuevaInscripcion)
    {
        var resultado = _gestionInscripciones.RegistrarInscripcion(nuevaInscripcion);
        if (resultado == null)
        {
            return BadRequest(new { mensaje = "No se pudo realizar la inscripcion. Verifique que el evento exista." });
        }

        return Ok(resultado);
    }

    [HttpGet("usuario/{idUsuario}")]
    public IActionResult MisIncripciones(int idUsuario)
    {
        var lista = _gestionInscripciones.ObtenerInscripcionesPorUsuario(idUsuario);
        return Ok(lista);
    }
}