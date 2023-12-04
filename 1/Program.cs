using System.Text.RegularExpressions;

namespace _1;

class Program
{
    private static Regex digitsRegex = new(@"(?(?=^(?:[^\d]*)(?:\d)(?:.*)(?:\d)[^\d]*$)^(?:[^\d]*)(?<firstdigit>\d)(?:.*)(?<seconddigit>\d)[^\d]*$|^(?:[^\d]*)(?<firstdigit>\d))", RegexOptions.Compiled);

    static void Main()
    {
        int result = (from line in File.ReadAllLines("input.txt")
            select digitsRegex.Match(line)
            into match
            let firstDigit = match.Groups["firstdigit"].Value
            let secondDigit = match.Groups["seconddigit"].Value == string.Empty
                ? firstDigit
                : match.Groups["seconddigit"].Value
            select Convert.ToInt32($"{firstDigit}{secondDigit}")).ToList().Sum();

        Console.Write($"Result: {result}");
    }
}