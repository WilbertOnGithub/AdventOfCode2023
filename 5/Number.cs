namespace _5;

public class Number
{
    public int Value { get; init; }
    private int LineNumber { get; init; }
    private int Index { get; init; }

    public bool ContainsSymbolInBoundingBox { get; set; }

    public Number(int value, int lineNumber, int index)
    {
        Value = value;
        LineNumber = lineNumber;
        Index = index;
    }

    public IEnumerable<(int line, int column)> GetBoundingBoxCoordinates()
    {
        int numberLength = Value.ToString().Length;

        // Top line coordinates
        for (int i = Index - 1; i < Index + numberLength + 1; i++)
        {
            yield return (LineNumber - 1, i);
        }

        // Middle line coordinates
        yield return (LineNumber, Index - 1);
        yield return (LineNumber, Index + numberLength);

        // Bottom line coordinates
        for (int i = Index - 1; i < Index + numberLength + 1; i++)
        {
            yield return (LineNumber + 1, i);
        }
    }
}