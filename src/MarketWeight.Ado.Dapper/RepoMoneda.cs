using System.Data;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;
using MarketWeight.Ado.Dapper;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;


namespace MarketWeight.Ado.Dapper;

public class RepoMoneda : RepoGenerico, IRepoMoneda
{
    public RepoMoneda(IDbConnection conexion) : base(conexion)
    {
    }

    public Moneda Alta(Moneda moneda)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xprecio", moneda.Precio);
        parametros.Add("@xcantidad", moneda.Cantidad);
        parametros.Add("@xnombre", moneda.Nombre);

        try
        {
            // Ejecuta el SP y obtiene el último ID insertado
            var id = Conexion.QuerySingle<uint>("AltaCriptoMoneda", parametros, commandType: CommandType.StoredProcedure);
            moneda.idMoneda = id;
            return moneda;
        }
        catch (DbException e)
        {
            if (e.ErrorCode == 1062)
                throw new ConstraintException($"La moneda {moneda.Nombre} ya ha sido ingresada.");
            throw;
        }
    }

    public Moneda? Detalle(uint indiceABuscar)
    {
        var consulta = $"SELECT * FROM Moneda WHERE idMoneda = {indiceABuscar}";
        var monedas = Conexion.QueryFirstOrDefault<Moneda>(consulta);
        return monedas;
    }

    public IEnumerable<Moneda> Obtener()
    {
        var consulta = "SELECT * FROM Moneda";
        var monedas = Conexion.Query<Moneda>(consulta);
        return monedas;
    }

    public IEnumerable<Moneda> ObtenerConCondicion(string condicion)
    {
        var consulta = $"SELECT * FROM Moneda WHERE {condicion}";
        var monedas = Conexion.Query<Moneda>(consulta);
        return monedas;
    }

    /*Async*/

    public async Task<Moneda> AltaAsync(Moneda moneda)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xprecio", moneda.Precio);
        parametros.Add("@xcantidad", moneda.Cantidad);
        parametros.Add("@xnombre", moneda.Nombre);

        try
        {
            var id = await Conexion.QuerySingleAsync<uint>(
                "AltaCriptoMoneda",
                parametros,
                commandType: CommandType.StoredProcedure
            );
            moneda.idMoneda = id;
            return moneda;
        }
        catch (DbException e)
        {
            if (e.ErrorCode == 1062)
                throw new ConstraintException($"La moneda {moneda.Nombre} ya ha sido ingresada.");
            throw;
        }
    }

    public async Task<Moneda?> DetalleAsync(uint indiceABuscar)
    {
        var consulta = $"SELECT * FROM Moneda WHERE idMoneda = {indiceABuscar}";
        var monedas = await Conexion.QueryFirstOrDefaultAsync<Moneda>(consulta);
        return monedas;
    }

    public async Task <IEnumerable<Moneda>> ObtenerAsync()
    {
        var consulta = "SELECT * FROM Moneda";
        var monedas = await Conexion.QueryAsync<Moneda>(consulta);
        return monedas;
    }

    public async Task<IEnumerable<Moneda>> ObtenerConCondicionAsync(string condicion)
    {
        var consulta = $"SELECT * FROM Moneda WHERE {condicion}";
        var monedas =await Conexion.QueryAsync<Moneda>(consulta);
        return monedas;
    }
}
