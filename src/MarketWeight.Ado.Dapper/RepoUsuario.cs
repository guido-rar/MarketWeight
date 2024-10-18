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
            Conexion.Execute("AltaUsuario", parametros);
        }
        catch (DbException e)
        {
            //DuplicateKeyEntry   
            if (e.ErrorCode == 1062)
            {
                throw new ConstraintException($"El Usuario {usuario.Nombre} ya ha sido ingresada.");
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

    public IEnumerable<UsuarioMoneda> ObtenerUsuarioMoneda()
    {
        var consulta = "SELECT * FROM UsuarioMoneda";
        var usuariosMoneda = Conexion.Query<UsuarioMoneda>(consulta);
        return usuariosMoneda;
    }



    public Usuario? Detalle(uint indiceABuscar)
    {
        var consulta = $"SELECT * FROM Usuario WHERE idUsuario = {indiceABuscar}";
        var usuarios = Conexion.QueryFirstOrDefault<Usuario>(consulta);
        return usuarios;
    }
    public void Ingreso(uint idusuario, decimal saldo)
    {

        var parametros = new DynamicParameters();
        parametros.Add("@xidusuario", idusuario);
        parametros.Add("@xsaldo", saldo);

        Conexion.Execute("IngresarDinero", parametros);
    }
    public void Compra(uint idusuario, decimal cantidad, uint idmoneda)
    {

        var parametros = new DynamicParameters();
        parametros.Add("@xidusuario", idusuario);
        parametros.Add("@xcantidad", cantidad);
        parametros.Add("@xidmoneda", idmoneda);

        Conexion.Execute("ComprarMoneda", parametros);
    }

    public void Transferencia( uint idmoneda, decimal cantidad, uint idusuarioTransfiere, uint idusuarioTransferido){
        var parametros = new DynamicParameters();
        parametros.Add("@xidMoneda", idmoneda);
        parametros.Add("@xcantidad", cantidad);
        parametros.Add("@xidUsuarioTransfiere", idusuarioTransfiere); 
        parametros.Add("@xidUsuarioTransferido", idusuarioTransferido);

        Conexion.Execute("Transferencia", parametros);
    }

    public void Vender(uint idusuario, decimal cantidad, uint idmoneda)
    {

        var parametros = new DynamicParameters();
        parametros.Add("@xidusuario", idusuario);
        parametros.Add("@xcantidad", cantidad);
        parametros.Add("@xidmoneda", idmoneda);

        Conexion.Execute("VenderMoneda", parametros);
    }

    public IEnumerable<Usuario> ObtenerPorCondicion (string condicion)
    {
        var consulta = $"SELECT U.nombre, U.saldo FROM Usuario U WHERE {condicion}";
        var usuarios = Conexion.Query<Usuario>(consulta);
        return usuarios;
    }

        public IEnumerable<UsuarioMoneda> ObtenerPorCondicionUsuarioMoneda (string condicion)
    {
        var consulta = $"SELECT UM.idUsuario, UM.cantidad FROM UsuarioMoneda UM WHERE {condicion}";
        var usuariosMonedas = Conexion.Query<UsuarioMoneda>(consulta);
        return usuariosMonedas;
    }
}
