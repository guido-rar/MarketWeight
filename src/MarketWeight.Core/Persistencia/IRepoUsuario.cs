namespace MarketWeight.Core.Persistencia;

public interface IRepoUsuario :
    IRepoAlta<Usuario>,
    IRepoListado<Usuario>,
    IRepoDetalle<Usuario, uint>
{
    public void Compra(uint idusuario, decimal cantidad, uint idmoneda)
    {}

    public void Vender(uint idusuario, decimal cantidad, uint idmoneda)
    {}

    public void Ingreso(uint idusuario, decimal saldo)
    {}
}
