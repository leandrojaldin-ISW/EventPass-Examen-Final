namespace tiendaweb_backend.Datos;
public abstract class Evento
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set;}
    public DateTime Fecha { get; set; }
    public string Ubicacion { get; set; }
    public int IdOrganizador { get; set; }
    
    //clase abstracta
    public abstract double CalcularCosto(int cantidadPersonas);
}