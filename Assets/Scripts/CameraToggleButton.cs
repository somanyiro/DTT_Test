using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggleButton : MonoBehaviour
{
    public MazeSpawner mazeSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChanged(bool newValue)
    {
        if (mazeSpawner is null)
            Debug.LogError("no maze spawner assigned to camera toggle");

        if (newValue)
            mazeSpawner.FreeCamera();
        else
            mazeSpawner.PositionCamera();

    }
    
}
