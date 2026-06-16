namespace tiendaweb_backend.Datos;

public class Inscripcion
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public int IdEvento { get; set; }
    public int CantidadPersonas { get; set; }
    public decimal TotalPagado { get; set; }
}