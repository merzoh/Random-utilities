namespace ConsoleUtil;
public static class ConsoleHelpers
{
    public static void DrawWindow(string prompt)
    {
        Console.Clear();
        DrawHorizontalLine();
        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        DrawHorizontalLine();
        DrawBorders();
        Console.SetCursorPosition((Console.WindowWidth - prompt.Length) / 2, ((int)(Console.WindowHeight * (1f / 3f))));
        Console.Write(prompt);
        void DrawHorizontalLine()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("=");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        void DrawBorders()
        {
            for (int i = 1; i < Console.WindowHeight - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("|");
                Console.ForegroundColor = ConsoleColor.White;
            }
            for (int i = 1; i < Console.WindowHeight - 1; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth - 1, i);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("|");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        Console.CursorVisible = false;
    }
    public static string ReadMaskedKey(char maskChar = '*')
    {
        string input = string.Empty;
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Enter)
                return input;
            else if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    Console.Write("\b \b");
                    input = input[..^1];
                }
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                input += keyInfo.KeyChar;
                Console.Write(maskChar);
            }
        }
    }
}
