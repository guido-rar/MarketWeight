class MarketWeight
{
    internal static bool salir = false;
}

class Program
{
    public static void Main() {

        do{
            
            Menu.ImprimirPantallaPrincipal();

        } while (MarketWeight.salir == false);
    
    }
}