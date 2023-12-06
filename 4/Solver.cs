using System.Reflection.PortableExecutable;

namespace _4;

public class Solver
{
    public int Solve()
    {
        IList<Game> games = new List<Game>();
        foreach (string line in File.ReadLines("input.txt"))
        {
            games.Add(Game.Create(line));
        }

        IList<int> sumOfCubes = new List<int>();
        foreach (var game in games)
        {
            int product = game.GetCubesNeeded().Where(cubesNeeded => cubesNeeded != 0)
                                               .Aggregate(1, (current, cubesNeeded) => current * cubesNeeded);
            sumOfCubes.Add(product);
        }

        return sumOfCubes.Sum();
    }
}