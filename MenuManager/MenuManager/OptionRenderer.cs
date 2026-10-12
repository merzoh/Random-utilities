namespace MenuManagerUtility;
public static class OptionRenderer
{
    private static readonly int _optionY = (int)(Console.WindowHeight * (2f / 3f)) - 1;
    public static void RenderOptions<T>(IEnumerable<T> options) where T : IMenuOptions
    {
        var optionList = options.ToList();
        Console.SetCursorPosition(optionList[0].OptionX, _optionY);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write(optionList[0].OptionName);
        Console.ResetColor();
        for (int i = 1; i < optionList.Count; i++)
        {
            var option = optionList[i];
            Console.SetCursorPosition(option.OptionX, _optionY);
            Console.Write(option.OptionName);
        }
    }
    public static void MoveSelector<T>(IEnumerable<T> options, int selectedIndex) where T : IMenuOptions
    {
        var optionList = options.ToList();
        var option = optionList[selectedIndex];
        Console.SetCursorPosition(option.OptionX, _optionY);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write(option.OptionName);
        Console.ResetColor();
    }
    public static void ClearSelectedOption<T>(IEnumerable<T> options, int selectedIndex) where T : IMenuOptions
    {
        var optionList = options.ToList();
        var option = optionList[selectedIndex];
        Console.SetCursorPosition(option.OptionX, _optionY);
        Console.Write(option.OptionName);
    }
}