using MarketWeight.Core;
using MarketWeight.Core.Persistencia;
using MySqlConnector;

namespace MarketWeight.Ado.Dapper.Test;

public class RepoUsuarioTest : TestBase
{
    IRepoUsuario _repo;
    public RepoUsuarioTest() : base()
        => _repo = new RepoUsuario(Conexion);
    
    [Fact]

    public void TraerOK()
    {
        var usuarios = _repo.Obtener();
        
        Assert.NotEmpty(usuarios);
        Assert.Contains(usuarios,
            m => m.Nombre == "Ana");
    }

    [Fact]

    public void IngresarDineroOK()
    {
        _repo.Ingreso(1, 7707m);

        _repo.Ingreso(2, 420m);

        _repo.Ingreso(3, 5000m);

        _repo.Ingreso(4, 6666m);

    }

    [Fact]
    public void AltaUsuarioOK()
    {
        Usuario usuarioWalter = new()
        {
            Nombre = "Walte",
            Apellido = "Beníte",
            Email = "waltercoocker@gmail.com",
            Password = "314159265358979"
        };

        Usuario usuarioJorge = new()
        {
            Nombre = "Jorge",
            Apellido = "Casco",
            Email = "JorgeCasco@gmail.com",
            Password = "jorge123"
        };

        Usuario usuarioGuido = new()
        {
            Nombre = "Guido",
            Apellido = "Gavilán",
            Email = "guidopepin@gmail.com",
            Password = "guidopepin123"
        };

        Usuario usuarioCarlos = new()
        {
            Nombre = "Carlos",
            Apellido = "Bello",
            Email = "carloselbello@gmail.com",
            Password = "carlos123"
        };


        _repo.Alta(usuarioWalter);
        _repo.Alta(usuarioJorge);
        _repo.Alta(usuarioGuido);
        _repo.Alta(usuarioCarlos);
    }

    [Fact]
    public void ComprarMonedaOK()
    {
        _repo.Compra(3, 0.5m, 2);

        _repo.Compra(2, 1m, 3);

        _repo.Compra(2, 0.5m, 1);
    }

    [Fact]
    public void ComprarMonedaFail()
    {
        var error =  Assert.ThrowsAny<Exception> (()=>_repo.Compra(6, 0.5m, 1));
        Assert.Contains("Insuficiente", error.Message);
    }

    [Fact]
    public void VenderMonedaOK()
    {
        _repo.Vender(2, 0.1m, 1);
    }

    [Fact]
    public void VenderMonedaFail()
    {
        var error =  Assert.ThrowsAny<Exception> (()=>_repo.Vender(5, 0.5m, 2));
        Assert.Contains("Insuficiente", error.Message);

        error =  Assert.ThrowsAny<Exception> (()=>_repo.Vender(6, 0.5m, 5));
        Assert.Contains("Insuficiente", error.Message);

    }

    [Fact]
    public void ObtenerPorCondicionOK()
    {
        var usuarios = _repo.ObtenerPorCondicion("saldo >= 1000");
        Assert.NotEmpty(usuarios);
    }
    [Fact]

    public void TransferenciaOK()
    {
        var usuariosMoneda1 = _repo.ObtenerPorCondicionUsuarioMoneda(2, 0.5m);/*string? userid, decimal cantidad*/
        _repo.Compra(2, 2.5m, 1);

        Assert.NotEmpty(usuariosMoneda1);

        _repo.Transferencia(2, 0.5m, 2, 6);

        var usuariosMoneda2 = _repo.ObtenerPorCondicionUsuarioMoneda(6, 0.5m);
        Assert.NotEmpty(usuariosMoneda2);
    }

        [Fact]
        public void TransferenciaFAIL()
    {

        var error =  Assert.ThrowsAny<Exception> (()=>_repo.Transferencia(2, 0.5m, 8, 6));
        Assert.Equal("Cantidad Insuficiente!", error.Message);

    }

}