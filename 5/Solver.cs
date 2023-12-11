﻿using System.Text.RegularExpressions;

namespace _5;

public partial class Solver
{
    private IList<Number> Numbers = new List<Number>();

    public void Solve()
    {
        int lineNumber = 0;
        foreach (string line in File.ReadLines("input.txt"))
        {
            foreach (Number number in ExtractNumbers(line, lineNumber))
            {
                Numbers.Add(number);
            }

            lineNumber++;
        }
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
}