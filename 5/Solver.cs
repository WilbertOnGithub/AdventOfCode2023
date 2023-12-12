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
            PrintCoordinates(number);
        }

        return Numbers.Where(x => x.ContainsSymbolInBoundingBox).Sum(x => x.Value);
    }

    private bool BoundingBoxHasSymbol(Number number)
    {
        foreach ((int line, int column) in number.GetBoundingBoxCoordinates())
        {
            try
            {
                if (!MatchPoint().IsMatch(matrix[line].Substring(column, 1)))
                {
                    return true;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        return false;
    }

    private void PrintCoordinates(Number number)
    {
        Console.WriteLine($"Value: {number.Value}");
        Console.WriteLine($"Symbol: {number.ContainsSymbolInBoundingBox}");
        foreach ((int line, int column) in number.GetBoundingBoxCoordinates())
        {
            try
            {
                Console.WriteLine(matrix[line].Substring(column, 1));
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }
        Console.WriteLine();
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

    [GeneratedRegex(@"(?<Number>\d{1,})")]
    private static partial Regex MatchNumbers();

    [GeneratedRegex(@"\.")]
    private static partial Regex MatchPoint();
}