namespace MarketWeight.Core.Persistencia;

public interface IRepoUsuario :
    IRepoAlta<Usuario>,
    IRepoListado<Usuario>,
    IRepoDetalle<Usuario, uint>
{
    public Usuario Alta(Usuario usuario);

    public void Compra(uint idusuario, decimal cantidad, uint idmoneda);

    public void Vender(uint idusuario, decimal cantidad, uint idmoneda);

    public void Ingreso(uint idusuario, decimal saldo);
    public void Transferencia( uint idmoneda, decimal cantidad, uint idusuarioTransfiere, uint idusuarioTransferido);

    public IEnumerable<Usuario> ObtenerPorCondicion (string condicion);

    public IEnumerable<UsuarioMoneda> ObtenerUsuarioMoneda();

    public IEnumerable<UsuarioMoneda> ObtenerPorCondicionUsuarioMoneda (uint? userid, decimal? cantidad);

    public Usuario? DetalleCompleto(uint idUsuario);

    /*async*/
    
    public Task<Usuario> AltaAsync(Usuario usuario);
    public Task<IEnumerable<Usuario>> ObtenerAsync();
    public Task<IEnumerable<UsuarioMoneda>> ObtenerUsuarioMonedaAsync();
    public Task<Usuario?> DetalleAsync(uint indiceABuscar);
    public Task IngresoAsync(uint idusuario, decimal saldo);
    public Task CompraAsync(uint idusuario, decimal cantidad, uint idmoneda);
    public Task TransferenciaAsync(uint idmoneda, decimal cantidad, uint idusuarioTransfiere, uint idusuarioTransferido);
    public Task VenderAsync(uint idusuario, decimal cantidad, uint idmoneda);
    public Task<IEnumerable<Usuario>> ObtenerPorCondicionAsync(string condicion);
    public Task<IEnumerable<UsuarioMoneda>> ObtenerPorCondicionUsuarioMonedaAsync(uint? userid, decimal? cantidad);
    public Task<Usuario?> DetalleCompletoAsync(uint idUsuario);
}
