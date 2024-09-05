namespace MarketWeight.Core.Persistencia;

public interface IRepoAlta<T>
{
    void Alta(T elemento);
}