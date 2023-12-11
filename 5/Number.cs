namespace _5;

public class Number
{
    public int Value { get; init; }
    private int LineNumber { get; init; }
    private int Index { get; init; }

    public Number(int value, int lineNumber, int index)
    {
        Value = value;
        LineNumber = lineNumber;
        Index = index;
    }
}