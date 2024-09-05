using System.Data;

namespace MarketWeight.Ado.Dapper;
public class RepoGenerico
{
    protected readonly IDbConnection Conexion;
    public RepoGenerico(IDbConnection conexion) => Conexion = conexion;
}
