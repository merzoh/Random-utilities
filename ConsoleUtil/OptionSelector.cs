using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUtil;

public class OptionSelector
{
    //Fields and Properties
    public int SelectedIndex { get; private set; } = 0;
    //Methods
    public void ChangeSelection(int optionCount, MenuOptions[] options, ConsoleKeyInfo keyInfo)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.LeftArrow:
                if (SelectedIndex != 0)
                {
                    OptionRenderer.DrawOptions(options, -1, SelectedIndex);
                    SelectedIndex--;
                }
                else
                {
                    OptionRenderer.WrapAround(options, 0, SelectedIndex);
                    SelectedIndex = options.Length - 1;
                }
                break;
            case ConsoleKey.RightArrow:
                if (SelectedIndex != optionCount - 1)
                {
                    OptionRenderer.DrawOptions(options, 1, SelectedIndex);
                    SelectedIndex++;
                }
                else
                {
                    OptionRenderer.WrapAround(options, 1, SelectedIndex);
                    SelectedIndex = 0;
                }
                break;
            default:
                break;
        }
    }
}