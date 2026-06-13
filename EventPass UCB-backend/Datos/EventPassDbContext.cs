using Microsoft.EntityFrameworkCore;

namespace tiendaweb_backend.Datos;

public class EventPassDbContext : DbContext
{
    // El constructor recibe la configuración de la conexión (tu appsettings.json)
    public EventPassDbContext(DbContextOptions<EventPassDbContext> options) : base(options) { }

    // Estas son las representaciones directas de tus tablas en SQL
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Inscripcion> Inscripciones { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }

    // Aquí le enseñamos a EF Core sobre tu Polimorfismo (Patrón TPH) 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evento>()
            .HasDiscriminator<string>("TipoEvento")
            .HasValue<EventoGratuito>("Gratuito")
            .HasValue<EventoPagado>("Pagado");

        base.OnModelCreating(modelBuilder);
    }
}