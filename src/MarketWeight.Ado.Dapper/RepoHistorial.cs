using System.Data;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;
using Dapper;
using System.Data.Common;

namespace MarketWeight.Ado.Dapper;

public class RepoHistorial : RepoGenerico, IRepoHistorial
{
    public RepoHistorial(IDbConnection conexion) : base(conexion)
    {
    }

    public void Alta(Historial historial)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xidMoneda", historial.idMoneda);
        parametros.Add("@xcantidad", historial.Cantidad);
        parametros.Add("@xcompra", historial.Compra);
        parametros.Add("@xidUsuario", historial.IdUsuario);

        try
        {
            Conexion.Execute("AltaHistorial", parametros);
        }
        catch (DbException e)
        {
            //DuplicateKeyEntry   
            if (e.ErrorCode == 1062)
            {
                throw new ConstraintException($"Este registro ya se hizo anteriormente.");
            }
            throw;
        }   
    }

    public Historial? Detalle(uint indiceABuscar)
    {
        var consulta = $"SELECT * FROM Moneda WHERE idMoneda = {indiceABuscar}";
        var registro = Conexion.QueryFirstOrDefault<Historial>(consulta);
        return registro;
    }

    public IEnumerable<Historial> Obtener()
    {
        var consulta = "SELECT * FROM Usuario";
        var registros = Conexion.Query<Historial>(consulta);
        return registros;
    }
}
