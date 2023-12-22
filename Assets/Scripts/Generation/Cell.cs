using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public bool visited = false;
    
    public int x;
    public int y;

    public Cell topNeighbour;
    public Cell rightNeighbour;
    public Cell bottomNeighbour;
    public Cell leftNeighbour;

}
