using System.Collections;

namespace _2;

class Program
{
    private static readonly Dictionary<string, int> dictionary = new()
    {
        {"0", 0},
        {"1", 1},
        {"2", 2},
        {"3", 3},
        {"4", 4},
        {"5", 5},
        {"6", 6},
        {"7", 7},
        {"8", 8},
        {"9", 9},
        {"zero", 0},
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9}
    };
    static void Main(string[] args)
    {
        IList<int> total = new List<int>();
        foreach (string line in File.ReadAllLines("input.txt"))
        {
            int firstDigit = dictionary[GetLowestIndex(line)];
            int secondDigit = dictionary.ContainsKey(GetHighestIndex(line)) ?
                dictionary[GetHighestIndex(line)] : firstDigit;

            total.Add(firstDigit * 10 + secondDigit);
        }

        Console.WriteLine($"Result: {total.Sum()}");
    }

    private static string GetLowestIndex(string line)
    {
        Dictionary<int, string> indexedKeys = new Dictionary<int, string>();
        foreach (var key in dictionary.Keys)
        {
            int index = line.IndexOf(key, StringComparison.Ordinal);
            if (index == -1)
            {
                continue;
            }
            indexedKeys.Add(index, key);
        }

        return indexedKeys[indexedKeys.Keys.Min()];
    }

    private static string GetHighestIndex(string line)
    {
        Dictionary<int, string> indexedKeys = new Dictionary<int, string>();
        foreach (var key in dictionary.Keys)
        {
            int index = line.LastIndexOf(key, StringComparison.Ordinal);
            if (index == -1)
            {
                continue;
            }
            indexedKeys.Add(index, key);
        }

        return indexedKeys.Any() ? indexedKeys[indexedKeys.Keys.Max()] : string.Empty;
    }
}