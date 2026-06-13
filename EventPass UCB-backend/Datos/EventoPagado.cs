namespace tiendaweb_backend.Datos;

public class EventoPagado : Evento
{
    public double PrecioEntrada { get; set; }

    public override double CalcularCosto(int cantidadPersonas)
    {
        return PrecioEntrada * cantidadPersonas;
    }
}