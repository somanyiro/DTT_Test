using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private Cell[,] cellGrid;
    private bool[,] mazeGrid;

    private Stack<Cell> cellStack = new Stack<Cell>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate(int width, int height)
    {
        //create arrays
        cellGrid = new Cell[width,height];
        mazeGrid = new bool[width * 2 + 1, height * 2 + 1];

        //make cell connections
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                cellGrid[i, j].x = i*2+1;
                cellGrid[i, j].y = j*2+1;
                
                if (j + 1 < height)
                    cellGrid[i, j].topNeighbour = cellGrid[i, j + 1];
                if (j - 1 >= 0)
                    cellGrid[i, j].bottomNeighbour = cellGrid[i, j - 1];
                if (i + 1 < width)
                    cellGrid[i, j].rightNeighbour = cellGrid[i + 1, j];
                if (i - 1 >= 0)
                    cellGrid[i, j].leftNeighbour = cellGrid[i - 1, j];
            }
        }
        
        //generation
        Cell currentCell = cellGrid[Random.Range(0, width),Random.Range(0, height)];
        currentCell.visited = true;
        cellStack.Push(currentCell);

        while (cellStack.Count > 0)
        {
            currentCell = cellStack.Pop();

            int[] directions = {0,1,2,3};
            
            directions.KnuthShuffle();

            foreach (int dir in directions)
            {
                switch (dir)
                {
                    case 0:
                        if (currentCell.topNeighbour is not null && currentCell.topNeighbour.visited == false)
                        {
                            mazeGrid[currentCell.x, currentCell.y + 1] = true;
                            currentCell.topNeighbour.visited = true;
                            cellStack.Push(currentCell);
                            cellStack.Push(currentCell.topNeighbour);
                        }
                        break;
                    case 1:
                        if (currentCell.rightNeighbour is not null && currentCell.rightNeighbour.visited == false)
                        {
                            mazeGrid[currentCell.x + 1, currentCell.y] = true;
                            currentCell.rightNeighbour.visited = true;
                            cellStack.Push(currentCell);
                            cellStack.Push(currentCell.topNeighbour);
                        }
                        break;
                    case 2:
                        if (currentCell.bottomNeighbour is not null && currentCell.bottomNeighbour.visited == false)
                        {
                            mazeGrid[currentCell.x, currentCell.y - 1] = true;
                            currentCell.bottomNeighbour.visited = true;
                            cellStack.Push(currentCell);
                            cellStack.Push(currentCell.topNeighbour);
                        }
                        break;
                    case 3:
                        if (currentCell.leftNeighbour is not null && currentCell.leftNeighbour.visited == false)
                        {
                            mazeGrid[currentCell.x - 1, currentCell.y] = true;
                            currentCell.leftNeighbour.visited = true;
                            cellStack.Push(currentCell);
                            cellStack.Push(currentCell.topNeighbour);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

    }

    void ClearMaze()
    {
        
    }
    
    
    
}
