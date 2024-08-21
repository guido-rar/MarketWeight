class Calculo
{
    internal static decimal CalcularPuntoEquilibrio(decimal cantOferta, decimal cantDemanda, decimal pOferta, decimal pDemanda)
    {
        return (cantDemanda - cantOferta) / (pOferta - pDemanda);
    }

    // Método para calcular oferta
    internal static decimal CalcularOferta(decimal cantOferta, decimal pOferta, decimal puntoEquilibrio)
    {
        return (pOferta * puntoEquilibrio) - cantOferta;
    }

    // Método para calcular demanda
    internal static decimal CalcularDemanda(decimal cantDemanda, decimal pDemanda, decimal puntoEquilibrio)
    {
        return (pDemanda * puntoEquilibrio) - cantDemanda;
    }
}