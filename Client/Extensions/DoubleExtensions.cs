using System.Globalization;

namespace Client;

public static class DoubleExtensions
{
    public static string AsString(this double number) => number.ToString(CultureInfo.InvariantCulture);
}
