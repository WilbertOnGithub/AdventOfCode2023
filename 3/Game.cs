using System.Text.RegularExpressions;

namespace _3;

public class Game
{
    public int Id { get; private set; }

    private IList<Set> Sets { get; } = new List<Set>();

    private Game (int id)
    {
        Id = id;
    }

    public bool IsValid(int nrRedCubes, int nrGreenCubes, int nrBlueCubes)
    {
        return Sets.All(x => x.IsValid(nrRedCubes, nrGreenCubes, nrBlueCubes));
    }

    public static Game Create(string line)
    {
        Regex gameIdRegex = new (@"^Game\s(?<GameId>\d{1,3}):\s(?<Remainder>.*$)");
        Match gameIdMatch = gameIdRegex.Match(line);
        int gameId = Convert.ToInt32(gameIdMatch.Groups["GameId"].Value);

        string remainder = gameIdMatch.Groups["Remainder"].Value;
        string[] sets = remainder.Split(";");

        var game = new Game(gameId);
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