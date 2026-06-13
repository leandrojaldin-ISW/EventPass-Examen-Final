namespace tiendaweb_backend.Datos;

public class Comentario
{
    public int Id { get; set; }
    public string Texto { get; set; }
 
    public string Autor { get; set; }
    public int IdEvento { get; set; }
}