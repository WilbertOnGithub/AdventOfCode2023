namespace _3;

public class Set(int nrRedCubes, int nrGreenCubes, int nrBlueCubes)
{
    private int NrRedCubes { get; } = nrRedCubes;

    private int NrGreenCubes { get; } = nrGreenCubes;

    private int NrBlueCubes { get; } = nrBlueCubes;

    public bool IsValid(int nrRedCubes, int nrGreenCubes, int nrBlueCubes)
    {
        return NrRedCubes <= nrRedCubes &&
               NrGreenCubes <= nrGreenCubes &&
               NrBlueCubes <= nrBlueCubes;
    }
}