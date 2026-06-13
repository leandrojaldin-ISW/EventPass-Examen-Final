using tiendaweb_backend.Datos;

namespace tiendaweb_backend.Negocio;

public class GestionEventos
{
    private readonly EventPassDbContext _context;
    
    //inyectamos el punete a la base de datos
    public GestionEventos(EventPassDbContext context)
    {
        _context = context;
    }

    //1. Listar: Ef Core hace el select automatico
    public List<Evento> ListaEventos()
    {
        return _context.Eventos.ToList();
    }
    
    //2. Crear> solo agregamos el onjeto y guardamos . EF CORE te devuleve el id automaticamente
    public Evento CrearEvento(Evento nuevoEvento)
    {
        _context.Eventos.Add(nuevoEvento);
        _context.SaveChanges();
        return nuevoEvento;
    }
    
    //3. Buscar por id: Find() busca la llae primaria automaticamente
    public Evento BusccarPorId(int idBuscar)
    {
        return _context.Eventos.Find(idBuscar);
    }
    
    //4. Actualizar : Buscamos el original , le cambiamos los datos y guardmos
    public Evento ActualizarEvento(int id, Evento evendoEditado)
    {
        var eventoExistente = _context.Eventos.Find(id);
        if (eventoExistente == null)
        {
            return null;
        }
        
        //Actualizamos los cambios , cambiamos datos y guardamos
        eventoExistente.Nombre = eventoExistente.Nombre;
        eventoExistente.Ubicacion = eventoExistente.Ubicacion;

        _context.SaveChanges(); //Core hace el update solo de lo que cambio
        return eventoExistente;
    }
    
    //5. Elininar : Buscamos , removemos de la lista y guardamos
    public bool EliminarEvento(int idABorrar)
    {
        var eventoExistente = _context.Eventos.Find(idABorrar);
        if (eventoExistente == null)
        {
            return false;
        }

        _context.Eventos.Remove(eventoExistente); //preparamos el delete
        int filasAfectadas = _context.SaveChanges(); //disparamos a SQL
        return filasAfectadas > 0;
    }
}

