using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float moveSpeed = 1f;
    
    private Vector3 prevMousePos;
    
    // Start is called before the first frame update
    void Start()
    {
        prevMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = GetComponent<Camera>();
        Vector2 mouseMovement = Input.mousePosition - prevMousePos;
        prevMousePos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 forward = transform.forward;
            Vector3 flatForward = new Vector3(forward.x, 0, forward.z).normalized;
            Vector3 flatRight = new Vector3(flatForward.z, 0, -flatForward.x);

            transform.position -= flatForward * mouseMovement.y * moveSpeed;
            transform.position -= flatRight * mouseMovement.x * moveSpeed;
        }

        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - Input.mouseScrollDelta.y, 1, 500);
    }
}
