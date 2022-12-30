using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static System.Text.RegularExpressions.RegexOptions;

namespace CustomTools.Utilities;

public static class StringUtility
{
    private const string _initialGroup = "Initial";
    private const string _remainderGroup = "Remainder";

    private const string _propertyNamePattern = $"^(?<{_initialGroup}>[A-Z])(?<{_remainderGroup}>[A-Za-z0-9]*)$";
    private static readonly Regex _propertyNameRegex = new(_propertyNamePattern, Compiled | CultureInvariant | Singleline);

    public static string Join(this IEnumerable<string> sequence, string separator = "")
    {
        return string.Join(separator, sequence);
    }

    /// <summary>
    /// Capitalizes the given string input. First letter will be capitalized and the following ones will be lowered.
    /// </summary>
    /// <param name="input">The input to be capitalized</param>
    public static void Capitalize(ref string input)
    {
        if (!string.IsNullOrEmpty(input))
            input = $"{input[0].ToString().ToUpper()}{input[1..].ToLower()}";
    }

    public static bool IsNotNullOrWhiteSpace(this string input)
    {
        return string.IsNullOrEmpty(input) is false;
    }

    public static IEnumerable<string> Sequence(int length, int quantity)
    {
        for (int current = 1; current <= quantity; current++)
        {
            string format = $$"""{0:D{{length}}}""";
            string value = string.Format(format, current);
            yield return value;
        }
    }

    public static string ToCamelCase(this string? input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        var match = _propertyNameRegex.Match(input);

        if (match.Success)
        {
            return $"{match.GroupValue(_initialGroup).ToUpperInvariant()}{match.GroupValue(_remainderGroup)}";
        }
        
        throw new ArgumentException($"Invalid argument: '{input}'", nameof(input));
    }

    public static string? Trimmed(this string? input)
    {
        if (input is null)
        {
            return default;
        }

        string trimmed = input.Trim();

        if (string.IsNullOrWhiteSpace(trimmed))
        {
            return default;
        }

        return trimmed;
    }

    public static int SafeCompare(string? first, string? second)
    {
        const int firstBeforeSecond = -1;
        const int equal = 0;
        const int secondBeforeFirst = 1;

        if (ReferenceEquals(first, second))
        {
            return equal;
        }

        if (first is null)
        {
            return secondBeforeFirst;
        }

        if (second is null)
        {
            return firstBeforeSecond;
        }

        return first.CompareTo(second);
    }

    public static int CompareTrimmed(string? first, string? second)
    {
        string? firstTrimmed = first.Trimmed();
        string? secondTrimmed = second.Trimmed();
        return SafeCompare(firstTrimmed, secondTrimmed);
    }
}