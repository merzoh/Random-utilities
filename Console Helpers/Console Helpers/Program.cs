class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        ConsoleHelpers.DrawOptions();
        Console.ReadKey(true);
    }
}

enum Options
{
    Message,
    Exit,
}

public static class ConsoleHelpers
{
    public static void DrawOptions()
    {
        int windowWidth = Console.WindowWidth;
        int windowHeight = Console.WindowHeight;
        int optionsCharacterCount = string.Join("", Enum.GetNames(typeof(Options))).Length;
        int padding = (windowWidth - optionsCharacterCount) / Enum.GetValues(typeof(Options)).Length;

        DrawLine();
        Console.SetCursorPosition(0, windowHeight - 1);
        DrawLine();
        Console.SetCursorPosition(0, windowHeight / 2);
        foreach (Options option in Enum.GetValues(typeof(Options)))
        {
            DrawPadding();
            Console.Write($"{option} ");
        }
        void DrawLine()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("=");
            }
        }
        void DrawPadding()
        {
            for (int i = 0; i < padding; i++)
            {
                Console.Write(" ");
            }
        }
    }
    public static string ReadMaskedKey(char maskChar = '*')
    {
        string input = string.Empty;
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return input;
            }
            else if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    Console.Write("\b \b");
                    input = input[..^1];
                }
            }
            else if(!char.IsControl(keyInfo.KeyChar))
            {
                input += keyInfo.KeyChar;
                Console.Write(maskChar);
            }
        }
    }
}