using System.Text.RegularExpressions;

namespace Core;

public static partial class InputValidator
{
    private static readonly Regex ValidInputRegex = ValidationRegex();

    public static bool IsValid(string? input) => !string.IsNullOrWhiteSpace(input) && ValidInputRegex.IsMatch(input);

    [GeneratedRegex(@"^[A-Za-z0-9,\s]+$")]
    private static partial Regex ValidationRegex();
}
