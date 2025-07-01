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

    /*async*/
    [Fact]
    public async Task TraerOKAsync()
    {
        var historiales = await _repo.ObtenerAsync();
        
        Assert.NotEmpty(historiales);
        Assert.Contains(historiales,
            h => h.IdUsuario == 2);
    }
}