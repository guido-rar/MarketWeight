using System.Numerics;

namespace MarketWeight.Core.Persistencia;

public interface IRepoDetalle<T, IS> where IS : IBinaryNumber<IS>
{
    /// <summary>
    /// Método para obtener un elemento del tipo T en base a un indice simple
    /// </summary>
    /// <param name="indiceABuscar">Indice por el cual hara la busqueda</param>
    /// <returns>Un elemento si encuentra, o null</returns>
    T? Detalle (IS indiceABuscar);

    Task<T?> DetalleAsync (IS indiceABuscar);
}
