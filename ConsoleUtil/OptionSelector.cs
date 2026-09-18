using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUtil;

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