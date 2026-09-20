using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUtil;

public class MenuManager<T> where T : Enum
{
    //PROPERTIES AND FIELDS
    private readonly MenuOptions[] options;
    private readonly OptionSelector selector;
    private int _optionY;
    private T[] Values { get; }
    private Type EnumType { get; }
    private int TotalChar { get; set; } = 0;
    private int Padding { get; set; }
    private int WindowWidth { get; set; }
    private int WindowHeight { get; set; }
    private int OptionY => (int)(Console.WindowHeight * (2f / 3f)) - 1;
    //METHODS
    public void UpdateOptionPositions()
    {
        int padding = (WindowWidth - TotalChar / (Values.Length + 1));
        int currentX = padding - 1;
        foreach (var option in options)
        {
            option.UpdatePos(currentX, OptionY);
            currentX += option.OptionName.Length + padding;
        }
    }
    public T Run(string prompt)
    {

        ConsoleHelpers.DrawWindow(prompt);
        OptionRenderer.DrawOptions(options, selector.SelectedIndex);
        ConsoleKeyInfo keyInfo = default;
        do
        {
            
            if (WindowHeight != Console.WindowHeight || WindowWidth != Console.WindowWidth)
            {
                UpdateOptionPositions();
                ConsoleHelpers.DrawWindow(prompt);
                OptionRenderer.DrawOptions(options, selector.SelectedIndex);
                WindowWidth = Console.WindowWidth;
                WindowHeight = Console.WindowHeight;
            }
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key != ConsoleKey.Enter)
                    selector.ChangeSelection(Values.Length, options, keyInfo);
            }
            else
                Thread.Sleep(100);
        } while (keyInfo.Key != ConsoleKey.Enter);
        return Values[selector.SelectedIndex];
    }
    //CONSTRUCTOR
    public MenuManager()
    {
        EnumType = typeof(T);
        Values = (T[])Enum.GetValues(EnumType);
        selector = new OptionSelector();
        options = new MenuOptions[Values.Length];
        foreach (var value in Values)
        {
            TotalChar += value.ToString().Length;
        }
        WindowWidth = Console.WindowWidth;
        WindowHeight = Console.WindowHeight;
        Padding = ((WindowWidth - TotalChar) / (Values.Length + 1));
        int currentX = Padding - 1;
        
        for (int i = 0; i < Values.Length; i++)
        {
            string name = Values.GetValue(i).ToString();
            options[i] = new MenuOptions(name, (currentX, OptionY));
            currentX += name.Length + Padding;

        }
    }


}
