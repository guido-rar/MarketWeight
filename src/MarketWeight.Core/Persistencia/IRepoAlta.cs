namespace MarketWeight.Core.Persistencia;

public interface IRepoAlta<T>
{
    T Alta(T elemento);
    Task<T> AltaAsync(T elemento);
}
