namespace MarketWeight.Core
{
    public class UsuarioMoneda
    {
        public required Usuario Usuario { get; set; }
        public required Moneda Moneda { get; set; }
        public required decimal Cantidad { get; set; }
    }
}