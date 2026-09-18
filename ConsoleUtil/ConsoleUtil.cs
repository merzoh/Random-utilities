namespace ConsoleUtil;

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
    public void UpdatePos(int posX, int posY) => StartPos = (posX, posY);


}
public class OptionSelector<T> where T : Enum
{
    //Fields and Properties

    private MenuOptions[] _options;
    private int _selectedIndex = 0;
    public int OptionY => (int)(Console.WindowHeight * (2f / 3f)) - 1;
    private int _windowWidth;
    private int _windowHeight;
    //Constructor that takes an enum type and creates a MenuOptions array based on the enum values
    public OptionSelector()
    {
        _windowWidth = Console.WindowWidth;
        _windowHeight = Console.WindowHeight;
        Type enumType = typeof(T);
        Array enumValues = Enum.GetValues(enumType);
        int padding = ((Console.WindowWidth - string.Join("", Enum.GetNames(enumType)).Length) / (enumValues.Length + 1));

        int currentX = padding - 1;

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
        ConsoleHelpers.DrawWindow(prompt);
        ConsoleHelpers.DrawOptions(_options);
        ConsoleKeyInfo keyInfo = default;
        do
        {

            if (_windowHeight != Console.WindowHeight || _windowWidth != Console.WindowWidth)
            {
                Console.Clear();
                ConsoleHelpers.UpdateOptionPositions<T>(_options, OptionY);
                ConsoleHelpers.DrawWindow(prompt);
                ConsoleHelpers.DrawOptions(_options);
                _windowWidth = Console.WindowWidth;
                _windowHeight = Console.WindowHeight;
            }
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (_selectedIndex != 0)
                        {
                            ConsoleHelpers.DrawOptions(_options, -1, _selectedIndex);
                            _options[_selectedIndex].IsSelected = false;
                            _selectedIndex--;
                            _options[_selectedIndex].IsSelected = true;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (_selectedIndex != values.Length - 1)
                        {
                            ConsoleHelpers.DrawOptions(_options, 1, _selectedIndex);
                            _options[_selectedIndex].IsSelected = false;
                            _selectedIndex++;
                            _options[_selectedIndex].IsSelected = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
                Thread.Sleep(100);


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

            if (option.IsSelected)
            {
                Console.SetCursorPosition((option.StartPos.x - 1), option.StartPos.y);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write($"[{option.OptionName}]");
                Console.ResetColor();
            }
            else
            {
                Console.SetCursorPosition(option.StartPos.x, option.StartPos.y);
                Console.Write(option.OptionName);
            }

        }
    }
    public static void DrawOptions(MenuOptions[] options, int direction, int currentSelection)
    {
        Console.SetCursorPosition(options[currentSelection].StartPos.x - 1, options[currentSelection].StartPos.y);
        Console.ResetColor();
        Console.Write(new string(' ', options[currentSelection].OptionName.Length + 2));
        Console.SetCursorPosition(options[currentSelection].StartPos.x, options[currentSelection].StartPos.y);
        Console.Write(options[currentSelection].OptionName);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.SetCursorPosition(options[currentSelection + direction].StartPos.x - 1, options[currentSelection + direction].StartPos.y);
        Console.Write($"[{options[currentSelection + direction].OptionName}]");
        Console.ResetColor();
    }
    public static void UpdateOptionPositions<T>(MenuOptions[] options, int posY) where T : Enum
    {
        Type enumType = typeof(T);
        Array enumValues = Enum.GetValues(typeof(T));
        int padding = ((Console.WindowWidth - string.Join("", Enum.GetNames(enumType)).Length) / (enumValues.Length + 1));
        int currentX = padding - 1;
        foreach (var option in options)
        {
            option.UpdatePos(currentX, posY);
            currentX += option.OptionName.Length + padding;
        }
    }
    public static void DrawWindow(string prompt)
    {
        Console.CursorVisible = false;
        DrawLine();
        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        DrawLine();
        Console.SetCursorPosition((Console.WindowWidth - prompt.Length) / 2, ((int)(Console.WindowHeight * (1f / 3f))));
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
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                input += keyInfo.KeyChar;
                Console.Write(maskChar);
            }
        }
    }
}
