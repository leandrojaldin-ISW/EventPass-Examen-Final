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
        //Buscamos el evento original en base a los datos
        var evento = _context.Eventos.Find(nuevaInscripcion.IdEvento);
        if (evento == null) //si no existe el evento cortamos aqui
        {
            return null;
        }
        
        //Polimorfismo
        //Llamamos al metodo calcularcosto. Ef core ya sabe si el evento es Gratuito o pagado
        //si es gratuito, este devuleve 0, si es pagado , multiplaca el precio por la cantidad de perosnas
        nuevaInscripcion.TotalPagado = evento.CalcularCosto(nuevaInscripcion.CantidadPersonas);
        
        //3.Guardamos la inscripcion con el precio final ya calculado
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