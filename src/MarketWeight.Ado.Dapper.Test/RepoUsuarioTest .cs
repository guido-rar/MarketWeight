using MarketWeight.Core;
using MarketWeight.Core.Persistencia;

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

    public void IngresarDinero()
    {
        _repo.Ingreso(1, 7707m);

        _repo.Ingreso(2, 420m);

        _repo.Ingreso(3, 5000m);

        _repo.Ingreso(4, 6666m);

    }

    [Fact]
    public void AltaUsuario()
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
    public void ComprarMoneda()
    {
        _repo.Compra(3, 0.5m, 2);

        _repo.Compra(2, 1m,3 );

        _repo.Compra(2, 0.5m,1 );
    }

    [Fact]
    public void VenderMoneda()
    {
        _repo.Vender(2, 0.5m, 2);

        _repo.Vender(3, 0.5m, 5);

        _repo.Vender(2, 0.1m, 1);
    }
}