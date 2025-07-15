namespace MarketWeight.Core;
public class Usuario
{
    public required uint idUsuario { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public  decimal Saldo { get; set; }
    public List<Historial>? Transacciones { get; set; }
    public List<UsuarioMoneda>? Billetera { get; set; }
}