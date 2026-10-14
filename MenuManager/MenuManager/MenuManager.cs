namespace MenuManagerUtility;
public class MenuManager
{
    //Fields
    private readonly int _promptY = (int)(Console.WindowHeight * (1f / 3f)) - 1;

    //Properties
    private int Padding { get; set; } = 0;
    private int SelectedIndex { get; set; } = 0;
    private int WindowWidth { get; set; } = Console.WindowWidth;
    private int WindowHeight { get; set; } = Console.WindowHeight;
    public MenuItem Run(IEnumerable<MenuItem> options, string prompt, ConsoleColor optionsColor = ConsoleColor.White)
    {
        Console.SetCursorPosition((WindowWidth - prompt.Length) / 2, _promptY);
        Console.Write(prompt);
        List<MenuItem> optionList = CalculateOptionPositions(options.ToList());
        OptionRenderer.RenderOptions(optionList, optionsColor);
        ConsoleKeyInfo keyInfo = default;
        Console.CursorVisible = false;
        do
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                OptionRenderer.ClearSelectedOption(optionList, SelectedIndex, optionsColor);
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex < 0)
                        SelectedIndex = optionList.Count - 1;
                }
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex >= optionList.Count)
                        SelectedIndex = 0;
                }
                OptionRenderer.MoveSelector(optionList, SelectedIndex);
            }
        } while (keyInfo.Key != ConsoleKey.Enter);
        return optionList[SelectedIndex];
    }
    public MenuItem Run(IEnumerable<MenuItem> options, string prompt, string message, ConsoleColor messageColor, ConsoleColor optionsColor = ConsoleColor.White)
    {
        
        Console.SetCursorPosition((WindowWidth - message.Length) / 2, _promptY + 1);
        Console.ForegroundColor = messageColor;
        Console.Write(message);
        return Run(options, prompt, optionsColor);
    }
    private List<MenuItem> CalculateOptionPositions(List<MenuItem> options)
    {
        List<MenuItem> positionedOptions = new List<MenuItem>(); 
        int optionCharCount = options.Sum(o => o.OptionName.Length);
        Padding  = (WindowWidth - optionCharCount) / (options.Count + 1);
        positionedOptions.Add(new MenuItem(options[0].OptionName, options[0].Execute, Padding));
        for (int i = 1; i < options.Count; i++)
        {
            positionedOptions.Add(new MenuItem(options[i].OptionName, options[i].Execute, positionedOptions[i - 1].OptionX + positionedOptions[i - 1].OptionName.Length + Padding));
        }
        return positionedOptions;
    }
}