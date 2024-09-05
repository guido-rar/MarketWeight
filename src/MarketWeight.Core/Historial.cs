namespace MarketWeight.Core
{
    public class Historial
    {
        public required uint IdHistorial { get; set; }
        public required uint IdUsuario { get; set; }
        public required uint idMoneda { get; set; }
        public required decimal Cantidad { get; set; }
        public required bool Compra { get; set; }
        public required DateTime FechaHora { get; set; }
    }
}
