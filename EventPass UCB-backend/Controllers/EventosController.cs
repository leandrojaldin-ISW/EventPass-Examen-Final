using Microsoft.AspNetCore.Mvc;
using tiendaweb_backend.Datos;
using tiendaweb_backend.Negocio;

namespace tiendaweb_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var eventosDb = _gestionEventos.ListaEventos();
    
        // Creamos una lista genérica vacía
        var eventosParaAngular = new List<object>();

        // Recorremos cada evento de la base de datos
        foreach (var e in eventosDb)
        {
            string tipoAsignado;
            double precioAsignado; // (o int/double, según lo hayas dejado)

            // Verificamos de qué tipo es la clase hija
            if (e is EventoPagado)
            {
                tipoAsignado = "Pagado";
            
                // Convertimos (Casteamos) el evento genérico a su forma Pagada
                // para poder acceder a la propiedad PrecioEntrada
                EventoPagado eventoConvertido = (EventoPagado)e;
                precioAsignado = eventoConvertido.PrecioEntrada;
            }
            else // Si no es pagado, asumimos que es Gratuito
            {
                tipoAsignado = "Gratuito";
                precioAsignado = 0;
            }

            // Armamos el objeto y lo metemos a la lista
            eventosParaAngular.Add(new {
                id = e.Id,
                nombre = e.Nombre,
                descripcion = e.Descripcion,
                fecha = e.Fecha,
                ubicacion = e.Ubicacion,
                tipoEvento = tipoAsignado,
                precioEntrada = precioAsignado
            });
        }

        return Ok(eventosParaAngular);
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
    public IActionResult Crear([FromBody] System.Text.Json.JsonElement datosEvento)
    {
        try 
        {
            // 1. Leemos el tipo de evento que nos manda Angular para saber qué clase usar
            string tipo = datosEvento.GetProperty("tipoEvento").GetString();
            
            // 2. Extraemos los campos que sí existen en tu clase Evento
            string nombre = datosEvento.GetProperty("nombre").GetString();
            string ubicacion = datosEvento.GetProperty("ubicacion").GetString();
            string descripcion = datosEvento.GetProperty("descripcion").GetString();
            string fechaString = datosEvento.GetProperty("fecha").GetString(); 
            DateTime fecha = DateTime.Parse(fechaString);
            
            // Asignamos un organizador por defecto para cumplir con la base de datos
            int idOrganizador = 1; 

            Evento nuevoEvento;

            // 3. Instanciamos la clase hija correspondiente
            if (tipo == "Gratuito")
            {
                nuevoEvento = new EventoGratuito 
                { 
                    Nombre = nombre, 
                    Ubicacion = ubicacion,
                    Descripcion = descripcion,
                    Fecha = fecha,
                    IdOrganizador = idOrganizador
                };
            }
            else if (tipo == "Pagado")
            {
                // Solo si es pagado, buscamos el precioEntrada
                double precio = datosEvento.GetProperty("precioEntrada").GetDouble();
                nuevoEvento = new EventoPagado 
                { 
                    Nombre = nombre, 
                    Ubicacion = ubicacion,
                    Descripcion = descripcion,
                    Fecha = fecha,
                    IdOrganizador = idOrganizador,
                    PrecioEntrada = precio 
                };
            }
            else 
            {
                return BadRequest(new { mensaje = "Tipo de evento no válido." });
            }

            // 4. Lo mandamos a tu capa de Negocio para guardar
            var eventoCreado = _gestionEventos.CrearEvento(nuevoEvento);
            return Ok(eventoCreado);
        }
        catch (Exception ex)
        {
             return BadRequest(new { mensaje = $"Error al procesar los datos: {ex.Message}" });
        }
    }

    [HttpPut("editar")]
public IActionResult Editar(int id, [FromBody] System.Text.Json.JsonElement datosEvento)
{
    try
    {
        // 1. Extraemos los campos del JSON igual que en el Crear
        string tipo = datosEvento.GetProperty("tipoEvento").GetString();
        string nombre = datosEvento.GetProperty("nombre").GetString();
        string ubicacion = datosEvento.GetProperty("ubicacion").GetString();
        string descripcion = datosEvento.GetProperty("descripcion").GetString();
        string fechaString = datosEvento.GetProperty("fecha").GetString(); 
        DateTime fecha = DateTime.Parse(fechaString);

        Evento eventoModificado;

        // 2. Instanciamos la clase hija correspondiente
        if (tipo == "Pagado")
        {
            double precio = datosEvento.GetProperty("precioEntrada").GetDouble();
            eventoModificado = new EventoPagado
            {
                Id = id, // ¡CLAVE! Pasamos el ID para que la BD sepa a quién actualizar
                Nombre = nombre,
                Ubicacion = ubicacion,
                Descripcion = descripcion,
                Fecha = fecha,
                IdOrganizador = 1, // Mantenemos tu usuario fantasma de prueba
                PrecioEntrada = precio
            };
        }
        else
        {
            eventoModificado = new EventoGratuito
            {
                Id = id, // ¡CLAVE!
                Nombre = nombre,
                Ubicacion = ubicacion,
                Descripcion = descripcion,
                Fecha = fecha,
                IdOrganizador = 1
            };
        }

        // 3. Lo mandamos a la capa de Negocio para hacer el Update
        // OJO: Aquí usa el nombre exacto de tu método en GestionEventos (ej. ModificarEvento, ActualizarEvento, etc.)
        _gestionEventos.ActualizarEvento(id, eventoModificado);

        return Ok(new { mensaje = "Evento actualizado correctamente" });
    }
    catch (Exception ex)
    {
        return BadRequest(new { mensaje = $"Error al actualizar: {ex.Message}" });
    }
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