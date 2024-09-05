using System.Data;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;
using Dapper;
using System.Data.Common;

namespace MarketWeight.Ado.Dapper;

public class RepoUsuario : RepoGenerico, IRepoUsuario
{
    public RepoUsuario(IDbConnection conexion) : base(conexion)
    {
    }


    public void Alta(Usuario usuario)
    {

        var parametros = new DynamicParameters();
        parametros.Add("@xnombre", usuario.Nombre);
        parametros.Add("@xapellido", usuario.Apellido);
        parametros.Add("@xemail", usuario.Email);
        parametros.Add("@xpass", usuario.Password);
        try
        {
            Conexion.Execute("altaCriptoMoneda", parametros);
        }
        catch (DbException e)
        {
            //DuplicateKeyEntry   
            if (e.ErrorCode == 1062)
            {
                throw new ConstraintException($"La moneda {usuario.Nombre} ya ha sido ingresada.");
            }
            throw;
        }
    }

    public IEnumerable<Usuario> Obtener()
    {
        var consulta = "SELECT * FROM Usuario";
        var usuarios = Conexion.Query<Usuario>(consulta);
        return usuarios;
    }

    public Usuario? Detalle(uint indiceABuscar)
    {
        var consulta = $"SELECT * FROM Usuario WHERE idUsuario = {indiceABuscar}";
        var usuarios = Conexion.QueryFirstOrDefault<Usuario>(consulta);
        return usuarios;
    }
}
