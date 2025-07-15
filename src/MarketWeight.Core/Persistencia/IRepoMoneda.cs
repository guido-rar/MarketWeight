namespace MarketWeight.Core.Persistencia;

public interface IRepoMoneda :
    IRepoAlta<Moneda>,
    IRepoListado<Moneda>,
    IRepoDetalle<Moneda, uint>
{
    IEnumerable<Moneda> ObtenerConCondicion(string condicion);

    
    Task<Moneda> AltaAsync(Moneda moneda);   
    Task<IEnumerable<Moneda>> ObtenerAsync();
    Task<Moneda?> DetalleAsync(uint indiceABuscar);
    Task<IEnumerable<Moneda>> ObtenerConCondicionAsync(string condicion);
}
