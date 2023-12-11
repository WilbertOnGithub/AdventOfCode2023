using System.Text.RegularExpressions;

namespace _5;

public partial class Solver
{
    private char[][] matrix;
    private IList<Number> Numbers = new List<Number>();

    public void Solve()
    {
        int lineNumber = 0;
        foreach (string line in File.ReadLines("input.txt"))
        {
            ExtractNumbers(line, lineNumber);
            lineNumber++;
        }
    }

    private void ExtractNumbers(string line, int lineNumber)
    {
        foreach (Match? match in MatchNumbers().Matches(line))
        {
            if (match != null)
            {
                int number = Convert.ToInt32(match.Groups["Number"].Value);
                int index = line.IndexOf(match.Groups["Number"].Value, StringComparison.Ordinal);
                Numbers.Add(new Number(number, lineNumber, index));
            }
        }

    }

    [GeneratedRegex(@"[^0-9]*(?<Number>\d{1,})")]
    private static partial Regex MatchNumbers();
}