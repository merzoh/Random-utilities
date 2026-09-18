class Program
{
    static void Main()
    {
        OptionSelector<Options> selector = new OptionSelector<Options>();
        selector.Run("Test");

    }
}

enum Options
{
    Message,
    Exit,
}
public class MenuOptions
{
    public string OptionName { get; private set; }
    public (int x, int y) StartPos { get; private set; }
    public bool IsSelected { get; set; }
    public MenuOptions(string optionName, (int x, int y) startPos)
    {
        OptionName = optionName;
        StartPos = startPos;
        IsSelected = false;
    }
}
public class OptionSelector<T> where T : Enum
{
    //Fields and Properties
    
    private MenuOptions[] _options;
    private int _selectedIndex = 0;
    public int OptionY => (int)(Console.WindowHeight * (2f / 3f));
    //Constructor that takes an enum type and creates a MenuOptions array based on the enum values
    public OptionSelector()
    {
        Type enumType = typeof(T);
        Array enumValues = Enum.GetValues(enumType);
        int padding = ((Console.WindowWidth - string.Join("", Enum.GetNames(enumType)).Length) / (enumValues.Length + 1));
        
        int currentX = padding;
        
        _options = new MenuOptions[enumValues.Length];
        for (int i = 0; i < enumValues.Length; i++)
        {
            string name = enumValues.GetValue(i).ToString();
            _options[i] = new MenuOptions(name, (currentX, OptionY));
            currentX += name.Length + padding;
        }
        _options[_selectedIndex].IsSelected = true;
    }
    //Methods
    public T Run(string prompt)
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        ConsoleHelpers.DrawOptionWindow(_options, prompt);
        ConsoleHelpers.DrawOptions(_options);
        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (_selectedIndex != 0)
                    {
                        ConsoleHelpers.DrawOptions(_options, true);
                        _selectedIndex--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (_selectedIndex != values.Length - 1)
                    {
                        ConsoleHelpers.DrawOptions(_options, false);
                        _selectedIndex++;
                    }
                    break;
                default:
                    break;
            }

        } while (keyInfo.Key != ConsoleKey.Enter);
        
        return values[_selectedIndex];
    }
    
    
}

public static class ConsoleHelpers
{
    public static void DrawOptions(MenuOptions[] options)
    {
        foreach (var option in options)
        {
            Console.SetCursorPosition(option.StartPos.x, option.StartPos.y);
            if (option.IsSelected)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write($"\b[{option.OptionName}]");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
                Console.Write(option.OptionName);
        }
    }
    public static void DrawOptions(MenuOptions[] options, bool moveLeft)
    {
        if (moveLeft)
        {

        }
    }
    public static void DrawOptionWindow(MenuOptions[] options, string prompt)
    {        
        DrawLine();
        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        DrawLine();
        Console.SetCursorPosition(0, ((int)(Console.WindowHeight * (1f / 3f))));
        Console.Write(prompt);
        void DrawLine()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("=");
                Console.ForegroundColor = ConsoleColor.White;
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