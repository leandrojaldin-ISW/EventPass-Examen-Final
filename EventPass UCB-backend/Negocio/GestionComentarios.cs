using tiendaweb_backend.Datos;
using System.Linq; // Necesario para usar .Where()

namespace tiendaweb_backend.Negocio;

public class GestionComentarios
{
    private readonly EventPassDbContext _context;
    
    //inyectamos el puente a la base de datos
    public GestionComentarios(EventPassDbContext context)
    {
        _context = context;
    }

    public List<Comentario> ObtenerTodos()
    {
        return _context.Comentarios.ToList();
    }

    public List<Comentario> ObtenerPorEvento(int idEvento)
    {
        // Entity Framework traduce este .Where a un "SELECT ... WHERE IdEvento = X" en SQL
        return _context.Comentarios.Where(c => c.IdEvento == idEvento).ToList();
    }

    public Comentario CrearComentario(Comentario nuevoComentario)
    {
        if (string.IsNullOrWhiteSpace(nuevoComentario.Texto))
        {
            return null;
        }
        
        //Verificamos si el evento existe directo en la base de datos
        var eventoExiste = _context.Eventos.Find(nuevoComentario.IdEvento);
        if (eventoExiste == null)
        {
            return null;
        }

        _context.Comentarios.Add(nuevoComentario);
        _context.SaveChanges(); //Sql le asigna el id automaticamente
        return nuevoComentario;
    }

    public Comentario ActualizarComentario(int id, string nuevoTexto)
    {
        if (string.IsNullOrWhiteSpace(nuevoTexto)) return null;

        var comentariosExistente = _context.Comentarios.Find(id);
        if (comentariosExistente != null)
        {
            comentariosExistente.Texto = nuevoTexto;
            _context.SaveChanges();
            return comentariosExistente;
        }

        return null;
    }

    public bool EliminarComentario(int id)
    {
        var comentarioExistente = _context.Comentarios.Find(id);
        if (comentarioExistente != null)
        {
            _context.Comentarios.Remove(comentarioExistente);
            _context.SaveChanges(); //Disparmos el delete
            return true;
        }

        return false;
    }
}