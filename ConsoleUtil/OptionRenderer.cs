using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUtil;

public static class OptionRenderer
{
    public static void DrawOptions(MenuOptions[] options, int direction, int currentSelection)
    {
        Console.ResetColor();
        Console.SetCursorPosition(options[currentSelection].StartPos.x - 1, options[currentSelection].StartPos.y);
        Console.Write($" {options[currentSelection].OptionName} ");
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.SetCursorPosition(options[currentSelection + direction].StartPos.x - 1, options[currentSelection + direction].StartPos.y);
        Console.Write($">{options[currentSelection + direction].OptionName}<");
        Console.ResetColor();
    }
    public static void DrawOptions(MenuOptions[] options, int currentSelection)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (i == currentSelection)
            {
                Console.SetCursorPosition((options[i].StartPos.x - 1), options[i].StartPos.y);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write($">{options[i].OptionName}<");
                Console.ResetColor();
            }
            else
            {
                Console.SetCursorPosition(options[i].StartPos.x, options[i].StartPos.y);
                Console.Write(options[i].OptionName);
            }
        }
    }
}