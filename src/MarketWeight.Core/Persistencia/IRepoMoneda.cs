namespace MarketWeight.Core.Persistencia;

public interface IRepoMoneda :
    IRepoAlta<Moneda>,
    IRepoListado<Moneda>,
    IRepoDetalle<Moneda, uint>
{
    public IEnumerable<Moneda> ObtenerConCondicion(string condicion);
}
