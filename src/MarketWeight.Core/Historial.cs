namespace MarketWeight.Core
{
    public class Historial
    {
        public required int IdHistorial { get; set; }
        public required int IdUsuario { get; set; }
        public Moneda moneda { get; set; }
        public required decimal Cantidad { get; set; }
        public required bool Compra { get; set; }
        public required DateTime FechaHora { get; set; }
    }
}