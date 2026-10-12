namespace MenuManagerUtility;

public interface IMenuOptions
{
    string OptionName { get; init; }
    int OptionX { get; init; }
    Action Execute { get; init; }

}