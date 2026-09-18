using System;
using System.Collections.Generic;
using System.Text;

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
