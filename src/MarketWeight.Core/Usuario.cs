namespace MarketWeight.Core;
public class Usuario
{
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public  decimal Saldo { get; set; }
    public List<Historial>? Transacciones { get; set; }
    private List<UsuarioMoneda>? Billetera { get; set; }
}