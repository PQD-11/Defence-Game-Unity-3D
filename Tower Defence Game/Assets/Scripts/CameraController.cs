using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Vector3 mouseDownPos;
    private Vector3 currentMousePos;
    private Vector3 offset; 

    void Update()
    {
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - Input.GetAxis("Mouse ScrollWheel") * 10f, 40f, 70f);

        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            currentMousePos = Input.mousePosition;
            offset = (currentMousePos - mouseDownPos) * 0.5f;
            offset.z = offset.y;
            // Debug.Log(offset);
            Vector3 newPosition = transform.position - offset * Time.deltaTime;

            newPosition.x = Mathf.Clamp(newPosition.x, -8f, 8f);
            newPosition.z = Mathf.Clamp(newPosition.z, -10f, 1f);
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            mouseDownPos = currentMousePos;
        }
    }
}
