class Program
{
    static void Main()
    {
        Console.Write("Enter sample text: ");
        string maskedInput = ConsoleHelpers.ReadMaskedKey();
        Console.WriteLine($"You entered: {maskedInput}");
    }
}

public static class ConsoleHelpers
{
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
            else
            {
                input += keyInfo.KeyChar;
                Console.Write(maskChar);
            }
        }
    }
}