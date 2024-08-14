
using System.Runtime.CompilerServices;

class Menu
{
    private static ConsoleKey _tecla;
    private static int _opcionActual = 0;
    private static string[] _opciones = ["Iniciar sesión", "Registrarse", "Mostrar Lista de Monedas", "Salir"];
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
                Console.WriteLine(Centrar($"> {opciones[i]} <"));
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(Centrar(opciones[i]));
            }
        }

        ReiniciarColores();
        
    }

    internal static void ImprimirPantallaPrincipal()
    {
        ReiniciarColores();
        Console.Clear();

        Console.WriteLine("\n\n\n");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(Centrar(ASCII.a));

        Console.WriteLine("\n");

        ReiniciarColores();

        ImprimirMenu(_opciones);

        Console.ForegroundColor = ConsoleColor.DarkGray;

        Console.WriteLine(ASCII.Creditos);

        System.Console.WriteLine(_opcionActual);

        EscucharTeclado();

        if (_tecla == ConsoleKey.DownArrow && _opcionActual < _opciones.Length -1)
        {
            _opcionActual++;
            ImprimirPantallaPrincipal();

        }
        
        else if (_tecla == ConsoleKey.DownArrow && _opcionActual >= _opciones.Length -1)
            _opcionActual = 0;

        if (_tecla == ConsoleKey.UpArrow && _opcionActual > 0)
        {
            _opcionActual--;
            ImprimirPantallaPrincipal();

        }

        else if (_tecla == ConsoleKey.UpArrow && _opcionActual <= 0)
            _opcionActual = _opciones.Length -1;
        
    }
}