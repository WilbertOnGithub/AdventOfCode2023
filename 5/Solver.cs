using System.Text.RegularExpressions;

namespace _5;

public partial class Solver
{
    private IList<Number> Numbers = new List<Number>();
    private IList<string> matrix = new List<string>();

    public int Solve()
    {
        int lineNumber = 0;
        foreach (string line in File.ReadLines("input.txt"))
        {
            matrix.Add(line);
            foreach (Number number in ExtractNumbers(line, lineNumber))
            {
                Numbers.Add(number);
            }

            lineNumber++;
        }

        foreach (Number number in Numbers)
        {
            number.ContainsSymbolInBoundingBox = BoundingBoxHasSymbol(number);
        }

        return Numbers.Where(x => x.ContainsSymbolInBoundingBox).Sum(x => x.Value);
    }

    private bool BoundingBoxHasSymbol(Number number)
    {
        bool found = false;

        foreach ((int line, int column) in number.GetBoundingBoxCoordinates())
        {
            try
            {
                Console.Write($"Line: {line} Column: {column} {matrix[line].AsSpan(column, 1)}");
                if (MatchSymbol()
                    .IsMatch(matrix[line].AsSpan(column, 1))) {
                    found = true;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        Console.WriteLine();
        // Everything checked, bounding box contains nothing.
        return found;
    }

    private IEnumerable<Number> ExtractNumbers(string line, int lineNumber)
    {
        foreach (Match? match in MatchNumbers().Matches(line))
        {
            if (match == null) continue;

            int number = Convert.ToInt32(match.Groups["Number"].Value);
            int index = line.IndexOf(match.Groups["Number"].Value, StringComparison.Ordinal);

            yield return new Number(number, lineNumber, index);
        }
    }

    [GeneratedRegex(@"[^0-9]*(?<Number>\d{1,})")]
    private static partial Regex MatchNumbers();

    [GeneratedRegex(@"[^.]")]
    private static partial Regex MatchSymbol();
}