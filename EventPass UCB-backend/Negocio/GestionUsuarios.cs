using tiendaweb_backend.Datos;

namespace tiendaweb_backend.Negocio;

public class GestionUsuarios
{
    private readonly EventPassDbContext _context;

    // Inyectamos el contexto de base de datos
    public GestionUsuarios(EventPassDbContext context)
    {
        _context = context;
    }

    // Método super limpio para registrar un usuario
    public bool Registrar(Usuario usuario)
    {
        _context.Usuarios.Add(usuario); // Preparamos el objeto
        int filasAfectadas = _context.SaveChanges(); // EF Core hace el INSERT automáticamente
        return filasAfectadas > 0;
    }

    public Usuario IniciarSesion(string correo, string password)
    {
        //convertimos la tabla en una lisat de c# para usar el indice [i]
        var todosLosUsuarios = _context.Usuarios.ToList();
        
        //recorremos todos lo usuarios
        for (int i = 0; i < todosLosUsuarios.Count; i++)
        {
            //verificar si el correo y password coinciden
            if (todosLosUsuarios[i].Correo == correo && todosLosUsuarios[i].Password == password)
            {
                return todosLosUsuarios[i];
            }
        }

        return null;
    }

    public Usuario AutenticarUsuario(string correo, string password)
    {
        var listaUsuarios = _context.Usuarios.ToList();

        for (int i = 0; i < listaUsuarios.Count; i++)
        {
            if (listaUsuarios[i].Correo == correo && listaUsuarios[i].Password == password)
            {
                return listaUsuarios[i];
            }
        }

        return null;
    }
}