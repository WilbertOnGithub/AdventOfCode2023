namespace _3;

class Program
{
    static void Main()
    {
        Solver solver = new Solver();

        Console.WriteLine($"Result: {solver.Solve(12, 13, 14)}");
    }
}