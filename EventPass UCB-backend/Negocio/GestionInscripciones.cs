using tiendaweb_backend.Datos;
using System.Linq;

namespace tiendaweb_backend.Negocio;

public class GestionInscripciones
{
    private readonly EventPassDbContext _context;

    public GestionInscripciones(EventPassDbContext context)
    {
        _context = context;
    }

    public Inscripcion RegistrarInscripcion(Inscripcion nuevaInscripcion)
    {
        var evento = _context.Eventos.Find(nuevaInscripcion.IdEvento);
        if (evento == null) 
        {
            return null;
        }
        
        // EL PUENTE: CalcularCosto sigue devolviendo double, pero lo convertimos 
        // a decimal aquí mismo para guardarlo sin romper las demás clases.
        nuevaInscripcion.TotalPagado = Convert.ToDecimal(evento.CalcularCosto(nuevaInscripcion.CantidadPersonas));
        
        _context.Inscripciones.Add(nuevaInscripcion);
        _context.SaveChanges();

        return nuevaInscripcion;
    }
    
    
    //Metodo para la visa del perfil del usuario
    public List<Inscripcion> ObtenerInscripcionesPorUsuario(int idUsuario)
    {
        var resultado = new List<Inscripcion>();

        var todasLasInscripciones = _context.Inscripciones.ToList();

        for (int i = 0; i < todasLasInscripciones.Count; i++)
        {
            if (todasLasInscripciones[i].IdUsuario == idUsuario)
            {
                resultado.Add(todasLasInscripciones[i]);
            }
        }

        return resultado;
    }
}