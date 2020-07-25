using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMovement : MonoBehaviour
{
    public float cameraSpeed;
    private Vector3 tempMousePosition;

    bool stopMoving;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            stopMoving = true;
        }
        if (stopMoving)
            return;
        tempMousePosition = Input.mousePosition - tempMousePosition;
        tempMousePosition = new Vector3(-tempMousePosition.y * cameraSpeed, tempMousePosition.x * cameraSpeed, 0);
        tempMousePosition = new Vector3(transform.eulerAngles.x + tempMousePosition.x, transform.eulerAngles.y + tempMousePosition.y, 0);
        transform.eulerAngles = tempMousePosition;
        tempMousePosition = Input.mousePosition;
    }
}