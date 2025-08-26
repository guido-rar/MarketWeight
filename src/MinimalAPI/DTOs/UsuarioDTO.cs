namespace MinimalAPI.DTOs;

public class UsuarioDTO
{
    public uint IdUsuario { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Email { get; set; } = "";
    public decimal Saldo { get; set; }
}
