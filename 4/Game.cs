using System.Text.RegularExpressions;

namespace _4;

public class Game
{
    public IEnumerable<int> GetCubesNeeded()
    {
        IList<int> minimumCubes = new List<int>();
        minimumCubes.Add(Sets.Max(x => x.NrRedCubes));
        minimumCubes.Add(Sets.Max(x => x.NrGreenCubes));
        minimumCubes.Add(Sets.Max(x => x.NrBlueCubes));

        return minimumCubes;
    }

    private IList<Set> Sets { get; } = new List<Set>();

    public static Game Create(string line)
    {
        Regex gameIdRegex = new (@"^Game\s(?<GameId>\d{1,3}):\s(?<Remainder>.*$)");
        Match gameIdMatch = gameIdRegex.Match(line);

        string remainder = gameIdMatch.Groups["Remainder"].Value;
        string[] sets = remainder.Split(";");

        var game = new Game();
        foreach (string set in sets)
        {
            string[] cubes = set.Split(",");

            (int red, int green, int blue) = ParseCubes(cubes);
            game.Sets.Add(new Set(red, green, blue));
        }

        return game;
    }

    private static (int red, int green, int blue) ParseCubes(string[] cubes)
    {
        Regex regex = new (@"\s?(?<Number>\d{1,2})\s(?<Color>.*)");

        int red = 0;
        int green = 0;
        int blue = 0;

        foreach (var cube in cubes)
        {
            Match match = regex.Match(cube);
            int number = Convert.ToInt32(match.Groups["Number"].Value);
            switch (match.Groups["Color"].Value)
            {
                case "red":
                    red += number;
                    break;
                case "green":
                    green += number;
                    break;
                case "blue":
                    blue += number;
                    break;
            }
        }

        return (red, green, blue);
    }
}