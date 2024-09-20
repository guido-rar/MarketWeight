using MarketWeight.Core;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.Ado.Dapper.Test;

public class RepoHistorialTest : TestBase
{
    IRepoHistorial _repo;
    public RepoHistorialTest() : base()
        => _repo = new RepoHistorial(Conexion);

    [Fact]
    public void TraerOK()
    {
        var historiales = _repo.Obtener();
        
        Assert.NotEmpty(historiales);
        Assert.Contains(historiales,
            h => h.IdUsuario == 2);
    }

    [Fact]
    public void TraerMonedas()
    {
        var monedasDelUSuario = _repo.ObtenerUsuarioMoneda(2);
        Assert.NotEmpty(monedasDelUSuario);
    }
}