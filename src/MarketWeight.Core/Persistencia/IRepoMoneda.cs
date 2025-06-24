namespace MarketWeight.Core.Persistencia;

public interface IRepoMoneda :
    IRepoAlta<Moneda>,
    IRepoListado<Moneda>,
    IRepoDetalle<Moneda, uint>
{
    public IEnumerable<Moneda> ObtenerConCondicion(string condicion);

    /*async*/
    public Task AltaAsync(Moneda moneda);

    public Task<Moneda?> DetalleAsync(uint indiceABuscar);

    public Task <IEnumerable<Moneda>> ObtenerAsync();

    public Task<IEnumerable<Moneda>> ObtenerConCondicionAsync(string condicion);
}
