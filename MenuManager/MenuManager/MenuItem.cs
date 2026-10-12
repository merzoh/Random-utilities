using MenuManagerUtility;

public record MenuItem(string OptionName, Action Execute, int OptionX = 0) : IMenuOptions;