using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    IMazeGenerator mazeGenerator = new IterativeDepthFirstGenerator();

    private bool[,] mazeGrid;
    
    public int mazeWidth = 20;
    public int mazeHeight = 10;
    
    public GameObject wallPrefab;

    public Camera camera;
    public IntegerInputField widthInput;
    public IntegerInputField heightInput;

    // Start is called before the first frame update
    void Start()
    {
        SpawnMaze();
        PositionCamera();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ClearMaze()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
            /*
            if (Application.isEditor)
                DestroyImmediate(child.gameObject);
            else
                Destroy(child.gameObject);
            */
        }
    }
    
    public void SpawnMaze()
    {
        mazeGrid = mazeGenerator.Generate(mazeWidth, mazeHeight);
        
        if (wallPrefab is null) Debug.LogError("no wall prefab was set");

        for (int i = 0; i < mazeGrid.GetLength(0); i++)
        {
            for (int j = 0; j < mazeGrid.GetLength(1); j++)
            {
                if (mazeGrid[i,j]) continue;
                var wall = Instantiate(wallPrefab, new Vector3(i, 0, j), Quaternion.identity);
                wall.transform.parent = gameObject.transform;
            }
        }
    }

    void PositionCamera()
    {
        if (camera is null) return;
        Vector3 centerPosition = new Vector3(mazeGrid.GetLength(0)/2, 0, mazeGrid.GetLength(1)/2);
        camera.transform.LookAt(centerPosition);
    }

    public void RuntimeSpawn()
    {
        if (widthInput is not null) mazeWidth = widthInput.Value;
        if (heightInput is not null) mazeHeight = heightInput.Value;

        ClearMaze();
        SpawnMaze();
        PositionCamera();
    }
    
}
