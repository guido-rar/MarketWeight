using MarketWeight.Core;
using MarketWeight.Core.Persistencia;
using MySqlConnector;

namespace MarketWeight.Ado.Dapper.Test;

public class RepoMonedaTest : TestBase
{
    IRepoMoneda _repo;
    public RepoMonedaTest() : base()
        => _repo = new RepoMoneda(Conexion);

    [Fact]

    public void CrearMonedaOK()
    {
        Moneda monedaPepe = new Moneda
        {
            Precio = 10m,
            Cantidad = 2m,
            Nombre = "pepe"
        };

        Moneda monedaVirgo = new Moneda
        {
            Precio = 300m,
            Cantidad = 5000m,
            Nombre = "VirgoCoin"
        };

        _repo.Alta(monedaPepe);
        _repo.Alta(monedaVirgo);
        
    }

    [Fact]
    public void CrearMonedaFail()
    {
        Moneda monedaDoge = new Moneda
        {
            Precio = 77m,
            Cantidad = 100m,
            Nombre = "DogeCoin"
        };

        Moneda monedaLitecoin = new Moneda
        {
            Precio = 77m,
            Cantidad = 100m,
            Nombre = "Litecoin"
        };

        var error =  Assert.Throws<MySqlException> (()=>_repo.Alta(monedaDoge));
        Assert.Equal("Moneda ya registrada :v", error.Message);

        error =  Assert.Throws<MySqlException> (()=>_repo.Alta(monedaLitecoin));
        Assert.Equal("Moneda ya registrada :v", error.Message);
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
    public void ObtenerConCondicionOK()
    {
        var monedas = _repo.ObtenerConCondicion("precio >= 100");

        Assert.NotEmpty(monedas);
    }
    /*Async*/
    [Fact]

    public async Task CrearMonedaOKAsync()
    {
        Moneda monedaPepe = new Moneda
        {
            Precio = 10m,
            Cantidad = 2m,
            Nombre = "pepe"
        };

        Moneda monedaVirgo = new Moneda
        {
            Precio = 300m,
            Cantidad = 5000m,
            Nombre = "VirgoCoin"
        };

        await _repo.Alta(monedaPepe);
        await _repo.Alta(monedaVirgo);
        
    }

    [Fact]
    public async Task CrearMonedaFailAsync()
    {
        Moneda monedaDoge = new Moneda
        {
            Precio = 77m,
            Cantidad = 100m,
            Nombre = "DogeCoin"
        };

        Moneda monedaLitecoin = new Moneda
        {
            Precio = 77m,
            Cantidad = 100m,
            Nombre = "Litecoin"
        };

        var error =  Assert.ThrowsAsync<MySqlException> (()=> await _repo.Alta(monedaDoge));
        Assert.Equal("Moneda ya registrada :v", error.Message);

        error =  Assert.ThrowsAsync<MySqlException> (()=>await _repo.Alta(monedaLitecoin));
        Assert.Equal("Moneda ya registrada :v", error.Message);
    }

    [Fact]
    public async Task TraerOKAsync()
    {
        var monedas = await _repo.Obtener();
        
        Assert.NotEmpty(monedas);
        Assert.Contains(monedas,
            m => m.Nombre == "Bitcoin" || m.Nombre == "pepe"  || m.Nombre == "dogeCoin" || m.Nombre == "VirgoCoin");
    }
    
    [Fact]
    public void ObtenerConCondicionOKAsync()
    {
        var monedas = await +_repo.ObtenerConCondicion("precio >= 100");

        Assert.NotEmpty(monedas);
    }

}