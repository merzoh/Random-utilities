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
public class MenuOptions
{
    public string OptionName { get; private set; }
    public (int x, int y) StartPos { get; private set; }
    public (int x, int y) EndPos { get; private set; }
    public bool IsSelected { get; set; }
    public MenuOptions(string optionName, (int x, int y) startPos, (int x, int y) endPos)
    {
        OptionName = optionName;
        StartPos = startPos;
        EndPos = endPos;
        IsSelected = false;
    }
    public void SetName(string name)
    {

    }
}
public class OptionSelector
{
    //Fields and Properties
    
    private MenuOptions[] _options;
    private int _selectedIndex = 0;
    //Constructor that takes an enum type and creates a MenuOptions array based on the enum values
    public OptionSelector(Enum enumType)
    {
        int padding = ((Console.WindowWidth - string.Join("", Enum.GetNames(enumType.GetType())).Length) / (Enum.GetValues(enumType.GetType()).Length + 1));
        int optionY = (int)(Console.WindowHeight * (2f / 3f));
        int currentX = padding;
        Array values = Enum.GetValues(enumType.GetType());
        _options = new MenuOptions[Enum.GetValues(enumType.GetType()).Length];
        for (int i = 0; i < Enum.GetValues(enumType.GetType()).Length; i++)
        {
            string name = values.GetValue(i).ToString();
            _options[i] = new MenuOptions(name, (currentX, optionY), (currentX + name.Length, optionY));
            currentX += name.Length + padding;
        }
    }
    //Methods

}

public static class ConsoleHelpers
{
    public static void DrawOptions(MenuOptions[] options, int selectedIndex)
    {
        int windowWidth = Console.WindowWidth;
        int windowHeight = Console.WindowHeight;
        

        DrawLine();
        Console.SetCursorPosition(0, windowHeight - 1);
        DrawLine();
        
        
        void DrawLine()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("=");
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