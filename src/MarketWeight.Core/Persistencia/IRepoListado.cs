namespace MarketWeight.Core.Persistencia;

public interface IRepoListado<T>
{
    IEnumerable<T> Obtener();
    
}
