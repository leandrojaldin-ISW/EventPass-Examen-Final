namespace tiendaweb_backend.Datos;

public class EventoGratuito : Evento
{
    public override double CalcularCosto(int cantidadPersonas)
    {
        return 0;
    }
}