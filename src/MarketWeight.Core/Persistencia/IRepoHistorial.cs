namespace MarketWeight.Core.Persistencia;

public interface IRepoHistorial :
    IRepoAlta<Historial>,
    IRepoListado<Historial>,
    IRepoDetalle<Historial, uint>
{
    /*async*/
    public Task<Historial?> DetalleAsync(uint indiceABuscar);
    public Task<IEnumerable<Historial>> ObtenerAsync();

    public Task AltaAsync(Historial historial);
    
}

