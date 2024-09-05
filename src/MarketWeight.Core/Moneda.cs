namespace MarketWeight.Core
{
    public class Moneda
    {
        public required uint IdMoneda { get; set; }
        public required decimal Precio { get; set; }
        public required decimal Cantidad { get; set; }
        public required string Nombre { get; set; }
    }
}

