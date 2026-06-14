using Microsoft.EntityFrameworkCore;

namespace tiendaweb_backend.Datos;

public class EventPassDbContext : DbContext
{
    public EventPassDbContext(DbContextOptions<EventPassDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Inscripcion> Inscripciones { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Configuración del Polimorfismo (TPH)
        modelBuilder.Entity<Evento>()
            .HasDiscriminator<string>("TipoEvento")
            .HasValue<EventoGratuito>("Gratuito")
            .HasValue<EventoPagado>("Pagado");

        // 2. Configuración del tipo de dato decimal para el Precio
        modelBuilder.Entity<EventoPagado>()
            .Property(e => e.PrecioEntrada)
            .HasColumnType("decimal(18,2)");

        base.OnModelCreating(modelBuilder);
    }
}