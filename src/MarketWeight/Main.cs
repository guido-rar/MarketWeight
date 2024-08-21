class MarketWeight
{
    public static int windowWidth = 156;
    public static int windowHeight = 44;

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

        Console.WindowWidth = MarketWeight.windowWidth;
        Console.WindowHeight = MarketWeight.windowHeight;

        MarketWeight.BuclePantallaPrincipal();
    }
}