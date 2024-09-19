using MarketWeight.Core;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.Ado.Dapper.Test;

public class RepoMonedaTest : TestBase
{
    IRepoMoneda _repo;
    public RepoMonedaTest() : base()
        => _repo = new RepoMoneda(Conexion);

    [Fact]
    public void TraerOK()
    {
        var monedas = _repo.Obtener();
        
        Assert.NotEmpty(monedas);
        Assert.Contains(monedas,
            m => m.Nombre == "Bitcoin");
    }
    
    [Fact]
    public void CrearMoneda()
    {
        Moneda monedaPepe = new Moneda
        {
            Precio = 10m,
            Cantidad = 2m,
            Nombre = "pepe"
        };

        _repo.Alta(monedaPepe);

        var monedas = _repo.Obtener();
        Assert.Contains(monedas, m => m.Nombre == "pepe");
    }
}