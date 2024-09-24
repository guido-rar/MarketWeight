using MarketWeight.Core;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.Ado.Dapper.Test;

public class RepoMonedaTest : TestBase
{
    IRepoMoneda _repo;
    public RepoMonedaTest() : base()
        => _repo = new RepoMoneda(Conexion);

    [Fact]

    public void CrearMoneda()
    {
        Moneda monedaPepe = new Moneda
        {
            Precio = 10m,
            Cantidad = 2m,
            Nombre = "pepe"
        };
        Moneda monedaDoge = new Moneda
        {
            Precio = 77m,
            Cantidad = 100m,
            Nombre = "DogeCoin"
        };

        Moneda monedaVirgo = new Moneda
        {
            Precio = 300m,
            Cantidad = 5000m,
            Nombre = "VirgoCoin"
        };

        _repo.Alta(monedaPepe);
        _repo.Alta(monedaDoge);
        _repo.Alta(monedaVirgo);
        
    }

    [Fact]
    public void TraerOK()
    {
        var monedas = _repo.Obtener();
        
        Assert.NotEmpty(monedas);
        Assert.Contains(monedas,
            m => m.Nombre == "Bitcoin" || m.Nombre == "pepe"  || m.Nombre == "dogeCoin" || m.Nombre == "VirgoCoin");
    }
    
    [Fact]
    public void ObtenerConCondicion()
    {
        var monedas = _repo.ObtenerConCondicion("precio >= 100");

        Assert.NotEmpty(monedas);
    }
}