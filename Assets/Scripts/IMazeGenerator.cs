using System.Collections.Generic;

public interface IMazeGenerator
{
    bool[,] Generate(int width, int height);
    List<(int row, int column)> GetGenerationHistory();
}