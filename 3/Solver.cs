namespace _3;

public class Solver
{
    public int Solve(int nrRedCubes, int nrGreenCubes, int nrBlueCubes)
    {
        IList<Game> games = new List<Game>();
        foreach (string line in File.ReadLines("input.txt"))
        {
            games.Add(Game.Create(line));
        }

        return games.Where(x => x.IsValid(nrRedCubes, nrGreenCubes, nrBlueCubes)).Sum(x => x.Id);
    }
}