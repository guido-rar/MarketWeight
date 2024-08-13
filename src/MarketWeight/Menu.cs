
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

    internal static void ImprimirTitulo()
    {
        ReiniciarColores();
        Console.Clear();

        Console.WriteLine("\n\n\n");

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
                    // ImprimirInicioSesion();
                break;
                
                case 1:
                    ImprimirRegistro();
                break;

                case 2:
                    // ImprimirListaCrypto();
                break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n");
                    Console.WriteLine(Centrar("Adios!"));
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n");
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
        Console.WriteLine(_tecla);
        Console.WriteLine(_opcionActual);
        Console.WriteLine(Convert.ToString(Console.WindowWidth));
        Console.WriteLine(Convert.ToString(Console.WindowHeight));

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

        Thread.Sleep(5000);

        Console.ForegroundColor = ConsoleColor.DarkGray;

        Console.WriteLine(ASCII.Creditos);
    }
}