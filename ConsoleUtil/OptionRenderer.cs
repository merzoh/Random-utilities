using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUtil;

public static class OptionRenderer
{
    public static void DrawOptions(MenuOptions[] options, int direction, int currentSelection)
    {
        Console.SetCursorPosition(options[currentSelection].StartPos.x - 1, options[currentSelection].StartPos.y);
        Console.ResetColor();
        // Console.Write(new string(' ', options[currentSelection].OptionName.Length + 2));
        Console.SetCursorPosition(options[currentSelection].StartPos.x - 1, options[currentSelection].StartPos.y);
        Console.Write($" {options[currentSelection].OptionName} ");
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.SetCursorPosition(options[currentSelection + direction].StartPos.x - 1, options[currentSelection + direction].StartPos.y);
        Console.Write($"[{options[currentSelection + direction].OptionName}]");
        Console.ResetColor();
    }
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
}