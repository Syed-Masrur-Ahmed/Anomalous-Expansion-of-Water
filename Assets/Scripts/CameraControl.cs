using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float rotationSpeed = 500.0f;
    private Vector3 mouseWorldPosStart;
    private float zoomScale = 10.0f;
    private float zoomMin = 0.1f;
    private float zoomMax = 10.0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.Mouse0))
        {
            CamOrbit();
        }
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftAlt))
        {
            mouseWorldPosStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftAlt))
        {
            Pan();
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
        
    }

    private void CamOrbit()
    {
        if (Input.GetAxis("Mouse Y")!=0 || Input.GetAxis("Mouse X") != 0)
        {
            float verticalInput = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            if ((verticalInput<0 && Camera.main.transform.position.y < 20) || (verticalInput > 0 && Camera.main.transform.position.y > 6.5))
            {
                transform.Rotate(Vector3.right, -verticalInput);
            }
            transform.Rotate(Vector3.up, horizontalInput, Space.World);
        }
    }
    private void Pan()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y")!=0)
        {
            Vector3 mouseWorldPosDiff = mouseWorldPosStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 var = transform.position + mouseWorldPosDiff;
            if (var.x>-8 && var.x<8 && var.y>0 && var.y<5)
            {
                transform.position += mouseWorldPosDiff;
            }
        }
    }
    private void Zoom(float zoomDiff)
    {
        if (zoomDiff != 0)
        {
            mouseWorldPosStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomDiff * zoomScale, zoomMin, zoomMax);
            Vector3 mouseWorldPosDiff = mouseWorldPosStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += mouseWorldPosDiff;
        }
    }
}
