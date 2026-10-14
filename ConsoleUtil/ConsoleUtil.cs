using System.Diagnostics.Tracing;

namespace ConsoleUtilities;
public static class ConsoleUtility
{
    public static string GetInput(string prompt)
    {
        Console.Clear();
        Console.SetCursorPosition((Console.WindowWidth - prompt.Length) / 2, (int)(Console.WindowHeight * (1f / 3f)));
        Console.Write(prompt);
        return Console.ReadLine();
    }
    public static void DrawWindow()
    {
        Console.Clear();                              
        DrawHorizontalLine();
        Console.SetCursorPosition(0, Console.WindowHeight - 1);                     
        DrawHorizontalLine();
        DrawBorders();
        
        
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
    public static void TypewriterEffect(string text, int delay, ConsoleColor textColor = ConsoleColor.White)
    {
        string formattedText = WrapText(text, Console.WindowWidth - 4);
        string skipText = "Press any key to skip...";
        string continueText = "Press any key to continue...";
        Console.CursorVisible = false;
        Console.SetCursorPosition((Console.WindowWidth - skipText.Length) / 2, Console.WindowHeight - 2);
        Console.Write(skipText);
        Console.SetCursorPosition(0,0);
        Console.ForegroundColor = textColor;
        for (int i = 0; i < formattedText.Length; i++)
        {
            char c = formattedText[i];
            if (Console.KeyAvailable)
            {
                Console.ReadKey(true); // Clear the key from the input buffer
                Console.Write(formattedText.Substring(i));
                break;
            }
            Console.Write(c);
            System.Threading.Thread.Sleep(delay);
        }
        Console.ResetColor();
        Console.SetCursorPosition(0, Console.WindowHeight - 2);
        Console.Write(new string(' ', Console.WindowWidth)); // Clear the skip text
        Console.SetCursorPosition((Console.WindowWidth - continueText.Length) / 2, Console.WindowHeight - 2);
        Console.Write(continueText);
        Console.ReadKey(true);
        Console.Clear();
    }
    public static string WrapText(string text, int maxLineLength)
    {
        var words = text.Split(' ');
        string wrappedText = "";
        string currentLine = "";
        foreach (string word in words)
        {
            if((currentLine + word).Length > maxLineLength)
            {
                wrappedText += currentLine.TrimEnd() + Environment.NewLine;
                currentLine = "";
            }
            currentLine += word + " ";
        }
        wrappedText += currentLine.TrimEnd();
        return wrappedText;
    }
}
