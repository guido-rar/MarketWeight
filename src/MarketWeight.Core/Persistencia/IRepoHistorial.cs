namespace MarketWeight.Core.Persistencia;

public interface IRepoHistorial :
    IRepoAlta<Historial>,
    IRepoListado<Historial>,
    IRepoDetalle<Historial, uint>
{
    public IEnumerable<Historial> ObtenerUsuarioMoneda(uint indiceUsuario);
}
