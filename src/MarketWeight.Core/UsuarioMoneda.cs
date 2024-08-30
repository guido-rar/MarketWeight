namespace MarketWeight.Core
{
    public class UsuarioMoneda
    {
        public required Usuario IdUsuario { get; set; }
        public required Moneda Moneda { get; set; }
        public required decimal Cantidad { get; set; }
    }
}