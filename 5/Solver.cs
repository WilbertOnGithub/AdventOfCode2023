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
        foreach ((int line, int column) boundingBoxCoordinate in number.GetBoundingBoxCoordinates())
        {
            try
            {
                if (MatchSymbol()
                    .IsMatch(matrix[boundingBoxCoordinate.line].Substring(boundingBoxCoordinate.column, 1)))
                {
                    Console.WriteLine($"Value found: {number.Value}");
                    return true;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                // Ignore
            }
        }

        // Everything checked, bounding box contains nothing.
        return false;
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