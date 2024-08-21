
using System.Runtime.CompilerServices;

class Menu
{
    private static ConsoleKey _tecla;
    private static int _opcionActual = 0;
    private static string[] _opciones = ["Calcular", "Mostrar Lista de Monedas (proximamente)", "Salir"];
    internal static string Centrar(string text)
    {
        string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        int longestLength = lines.Max(line => line.Length);

        string spaces = new(' ', (Console.WindowWidth - longestLength) / 2);

        string centeredText = string.Join(Environment.NewLine, lines.Select(line => spaces + line));

        return centeredText;
    }

    internal static void ReiniciarColores()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal static ConsoleKey EscucharTeclado()
    {

        _tecla = Console.ReadKey(true).Key;

        return _tecla;
    }

    internal static void ImprimirMenu(string[] opciones)
    {
        for (int i = 0; i < opciones.Length; i++)
        {
            if (_opcionActual == i)
            {
                ReiniciarColores();
                Console.WriteLine(Centrar($"> {opciones[i]} <\n"));
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(Centrar(opciones[i]) + "\n");
            }
        }

        ReiniciarColores();
    }

    internal static void ImprimirTitulo()
    {
        ReiniciarColores();
        Console.Clear();

        Console.WriteLine("\n");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(Centrar(ASCII.a));

        Console.WriteLine("\n");

        ReiniciarColores();
    }

    internal static void NavegarMenu()
    {
        EscucharTeclado();
        
        if (_tecla == ConsoleKey.DownArrow && _opcionActual < _opciones.Length -1)
            _opcionActual++;

        else if (_tecla == ConsoleKey.DownArrow && _opcionActual >= _opciones.Length -1)
            _opcionActual = 0;
        

        if (_tecla == ConsoleKey.UpArrow && _opcionActual > 0)
            _opcionActual--;
        

        else if (_tecla == ConsoleKey.UpArrow && _opcionActual <= 0)
            _opcionActual = _opciones.Length -1;

        if (_tecla == ConsoleKey.Enter)
        {
            switch(_opcionActual)
            {
                case 0:
                    MarketWeight.salir = true;
                    ImprimirPedido();
                break;
                
                case 1:
                    //IMprimir REgistro de calculos realizados.
                break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine(Centrar("Adios!!!"));
                    Console.WriteLine(Centrar("Gracias por utilizar nuestros servicios."));
                    Console.WriteLine("\n\n\n");
                    MarketWeight.salir = true;
                break;

                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Error! Opcion no disponible");
                break;
            }
        }
    }

    internal static void ImprimirPantallaPrincipal()
    {
        ImprimirTitulo();

        ImprimirMenu(_opciones);

        Console.ForegroundColor = ConsoleColor.DarkGray;

        Console.WriteLine(ASCII.Creditos);

        Console.WriteLine("Esta línea debe de caber por completo dentro de la pantalla para que el programa funcione correctamente.");
        Console.WriteLine(new string('-', MarketWeight.windowWidth));

        Console.WriteLine(_tecla);
        Console.WriteLine(_opcionActual);

        NavegarMenu();
    }

    internal static bool VerificarEmailValido(string email)
    {
        string trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith(".")) {
            return false;
        }

        try {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch {
            return false;
        }
    }

    internal static void ImprimirRegistro()
    {
        ImprimirTitulo();

        string[] inputs = ["Nombre", "Apellido", "Email", "Contraseña", "Confirmar Contraseña"];
        string[] datos = new string[inputs.Length];

        for (int i = 0; i < inputs.Length; i++)
        {
            Console.Write(Centrar($"{inputs[i]}: "));
            datos[i] = Console.ReadLine();
        }

        foreach (string x in datos)
            Console.Write($"{x}, ");

        Thread.Sleep(2000);

        Console.ForegroundColor = ConsoleColor.DarkGray;

        Console.WriteLine(ASCII.Creditos);

    }

    /*Pedido de datos*/

    internal static void ImprimirPedido ()
    {
        ImprimirTitulo();
        MarketWeight.salir = true;

        string[] inputs = ["Nombre del producto", "Cantidad Oferta", "Cantidad Demanda", "Precio Oferta", "Precio Demanda"];
        string[] datos = new string[inputs.Length];

        for (int i = 0; i < inputs.Length; i++)
        {
            Console.Write(Centrar($"{inputs[i]}: "));
            datos[i] = Console.ReadLine();
        }

        string  NombreActivo = (datos[0]);
        decimal cantidadOferta = decimal.Parse(datos[1]);
        decimal cantidadDemanda = decimal.Parse(datos[2]);
        decimal precioOferta = decimal.Parse(datos[3]);
        decimal precioDemanda = decimal.Parse(datos[4]);
        

        MostrarResultados(NombreActivo, cantidadOferta, cantidadDemanda, precioOferta, precioDemanda);
    }

    /*Metodo de calculo de propiedades*/

    internal static decimal CalcularPuntoEquilibrio(decimal cantOferta, decimal cantDemanda, decimal pOferta, decimal pDemanda)
    {
        return (cantDemanda - cantOferta) / (pOferta + pDemanda);
    }

    // Método para calcular oferta
    internal static decimal CalcularOferta(decimal cantOferta, decimal pOferta, decimal puntoEquilibrio)
    {
        return cantOferta + (pOferta * puntoEquilibrio);
    }

    // Método para calcular demanda
    internal static decimal CalcularDemanda(decimal cantDemanda, decimal pDemanda, decimal puntoEquilibrio)
    {
        return cantDemanda - (pDemanda * puntoEquilibrio);
    }

    internal static string  PropiedadMercado (decimal cantDemanda, decimal cantOferta)
    {
        if(cantDemanda > cantOferta)
        {
            return "    ↓  Activo en baja";
        }

        if(cantDemanda < cantOferta)
        {
            return "    ↑  Activo en alza";
        } 

        return "    =  Activo igual";
    }

    /*Muestra de resultados*/

    internal static void MostrarResultados(string NActivo, decimal cantOferta, decimal cantDemanda, decimal pOferta, decimal pDemanda)
    {
        ImprimirTitulo();

        decimal puntoEquilibrio = Calculo.CalcularPuntoEquilibrio(cantOferta, cantDemanda, pOferta, pDemanda);
        decimal oferta = Calculo.CalcularOferta(cantOferta, pOferta, puntoEquilibrio);
        decimal demanda = Calculo. CalcularDemanda(cantDemanda, pDemanda, puntoEquilibrio);

        Console.WriteLine(Centrar("Resultados Finales:"));
        Console.WriteLine(Centrar(""));
        Console.WriteLine(Centrar($"Nombre del Activo:" + NActivo));
        Console.WriteLine(Centrar($"Precio de Demanda: " + pDemanda.ToString("F2") + "  | Precio de Oferta: "  + pOferta.ToString("F2")));
        Console.WriteLine(Centrar("Volumen de Bien: " + demanda.ToString("F2") + "      | Precio de Equilibrio: " + puntoEquilibrio.ToString("F2")));
        Console.WriteLine(Centrar("El resultado es:" + PropiedadMercado(cantDemanda, cantOferta)));
        
        Console.WriteLine("\n\n");

        Console.WriteLine(Centrar("Presione [Enter] para volver al inicio."));

        if(EscucharTeclado() == ConsoleKey.Enter)
        {
            MarketWeight.salir = false;
            MarketWeight.BuclePantallaPrincipal();
        }
    }
}