using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUtil;

public class OptionSelector<T> where T : Enum
{
    //Fields and Properties

    private readonly MenuOptions[] _options;
    private int SelectedIndex { get; set; } = 0;
    private static int OptionY => (int)(Console.WindowHeight * (2f / 3f)) - 1;
    private int WindowWidth { get; set; }
    private int WindowHeight { get; set; }
    private T[] Values { get; }
    private Type EnumType { get; }
    private int TotalChar { get; set; } = 0;
    private int Padding { get; set; }

    //Constructor that takes an enum type and creates a MenuOptions array based on the enum values
    public OptionSelector()
    {
        EnumType = typeof(T);
        Values = (T[])Enum.GetValues(EnumType);
        foreach (var value in Values)
        {
            TotalChar += value.ToString().Length;
        }
        WindowWidth = Console.WindowWidth;
        WindowHeight = Console.WindowHeight;
        Padding = ((WindowWidth - TotalChar) / (Values.Length + 1));

        int currentX = Padding - 1;

        _options = new MenuOptions[Values.Length];
        for (int i = 0; i < Values.Length; i++)
        {
            string name = Values.GetValue(i).ToString();
            _options[i] = new MenuOptions(name, (currentX, OptionY));
            currentX += name.Length + Padding;

        }
        _options[SelectedIndex].IsSelected = true;
    }
    //Methods
    public void UpdateOptionPositions()
    {
        
        int padding = (WindowWidth - TotalChar / (Values.Length + 1));
        int currentX = padding - 1;
        int posY = OptionY;
        foreach (var option in _options)
        {
            option.UpdatePos(currentX, posY);
            currentX += option.OptionName.Length + padding;
        }
    }
    public T Run(string prompt)
    {
        
        ConsoleHelpers.DrawWindow(prompt);
        OptionRenderer.DrawOptions(_options);
        ConsoleKeyInfo keyInfo = default;
        do
        {
            if (WindowHeight != Console.WindowHeight || WindowWidth != Console.WindowWidth)
            {
                Console.Clear();
                UpdateOptionPositions();
                ConsoleHelpers.DrawWindow(prompt);
                OptionRenderer.DrawOptions(_options);
                WindowWidth = Console.WindowWidth;
                WindowHeight = Console.WindowHeight;
            }
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (SelectedIndex != 0)
                        {
                            OptionRenderer.DrawOptions(_options, -1, SelectedIndex);
                            _options[SelectedIndex].IsSelected = false;
                            SelectedIndex--;
                            _options[SelectedIndex].IsSelected = true;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (SelectedIndex != Values.Length - 1)
                        {
                            OptionRenderer.DrawOptions(_options, 1, SelectedIndex);
                            _options[SelectedIndex].IsSelected = false;
                            SelectedIndex++;
                            _options[SelectedIndex].IsSelected = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
                Thread.Sleep(100);
        } while (keyInfo.Key != ConsoleKey.Enter);
        return Values[SelectedIndex];
    }
}