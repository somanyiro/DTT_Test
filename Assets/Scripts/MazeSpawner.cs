using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    IMazeGenerator mazeGenerator = new IterativeDepthFirstGenerator();

    private bool[,] mazeGrid;
    private GameObject[,] spawnedWalls;
    
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
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!Application.isPlaying)
                DestroyImmediate(transform.GetChild(i).gameObject, true);
            else
                Destroy(transform.GetChild(i).gameObject);
        }
    }
    
    public void SpawnMaze()
    {
        mazeGrid = mazeGenerator.Generate(mazeWidth, mazeHeight);
        spawnedWalls = new GameObject[mazeGrid.GetLength(0), mazeGrid.GetLength(1)];
        
        if (wallPrefab is null) Debug.LogError("no wall prefab was set");

        for (int i = 0; i < mazeGrid.GetLength(0); i++)
        {
            for (int j = 0; j < mazeGrid.GetLength(1); j++)
            {
                if (mazeGrid[i,j]) continue;
                var wall = Instantiate(wallPrefab, new Vector3(i, 0, j), Quaternion.identity);
                wall.transform.parent = gameObject.transform;
                spawnedWalls[i, j] = wall;
            }
        }
    }

    void PositionCamera()
    {
        if (camera is null) return;
        Vector3 centerPosition = new Vector3(mazeGrid.GetLength(0)/2, 0, mazeGrid.GetLength(1)/2);
        camera.transform.position = centerPosition + Vector3.up * 100;

        if (mazeGrid.GetLength(1) > mazeGrid.GetLength(0))
        {
            camera.transform.rotation = Quaternion.Euler(90,0,0);
            camera.orthographicSize = mazeGrid.GetLength(1)/2+1;
        }
        else
        {
            camera.transform.rotation = Quaternion.Euler(90,90,0);
            camera.orthographicSize = mazeGrid.GetLength(0)/2+1;
        }
    }

    public void RuntimeSpawn()
    {
        if (widthInput is not null) mazeWidth = widthInput.Value;
        if (heightInput is not null) mazeHeight = heightInput.Value;

        ClearMaze();
        SpawnMaze();
        PositionCamera();
    }

    public void StepByStepSpawn()
    {
        StopCoroutine(StepByStepRoutine());
        StartCoroutine(StepByStepRoutine());
    }

    IEnumerator StepByStepRoutine()
    {
        if (widthInput is not null) mazeWidth = widthInput.Value;
        if (heightInput is not null) mazeHeight = heightInput.Value;

        ClearMaze();
        
        mazeGrid = mazeGenerator.Generate(mazeWidth, mazeHeight);
        spawnedWalls = new GameObject[mazeGrid.GetLength(0), mazeGrid.GetLength(1)];
        
        if (wallPrefab is null) Debug.LogError("no wall prefab was set");

        for (int i = 0; i < mazeGrid.GetLength(0); i++)
        {
            for (int j = 0; j < mazeGrid.GetLength(1); j++)
            {
                var wall = Instantiate(wallPrefab, new Vector3(i, 0, j), Quaternion.identity);
                wall.transform.parent = gameObject.transform;
                spawnedWalls[i, j] = wall;
            }
        }
        
        PositionCamera();

        var generationHistory = mazeGenerator.GetGenerationHistory();

        foreach (var step in generationHistory)
        {
            Destroy(spawnedWalls[step.row, step.column]);
            yield return new WaitForSeconds(0.01f);
        }
        
    }
    
}
