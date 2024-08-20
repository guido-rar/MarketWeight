class MarketWeight
{

    internal static bool salir = false;

    public static void BuclePantallaPrincipal()
    {
        do{
            
            Menu.ImprimirPantallaPrincipal();

        } while (salir == false);
    }
}

class Program
{
    public static void Main() {
        MarketWeight.BuclePantallaPrincipal();
    }
}