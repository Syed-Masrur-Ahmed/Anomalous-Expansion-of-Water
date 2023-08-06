using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float rotationSpeed = 500.0f;
    private Vector3 mouseWorldPosStart;
    // private float zoomScale = 10.0f;
    // private float zoomMin = 0.1f;
    // private float zoomMax = 10.0f;

    void Update() {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.Mouse0)) {
            CamOrbit();
        }
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftAlt)) {
            mouseWorldPosStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftAlt)) {
            Pan();
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
        
    }

    private void CamOrbit() {
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0) {
            float verticalInput = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            var mainCamY = Camera.main.transform.position.y;
            if ((verticalInput < 0 && mainCamY < 10) || (verticalInput > 0 && mainCamY > 4))
            {
                transform.Rotate(Vector3.right, -verticalInput);
            }
            /*transform.Rotate(Vector3.up, horizontalInput, Space.World);*/
        }
    }

    private void Pan() {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
            Vector3 mouseWorldPosDiff = mouseWorldPosStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 check = transform.position + mouseWorldPosDiff;
            if (check.x > -11 && check.x < 10 && check.y > -8 && check.y < 8) {
                transform.position += mouseWorldPosDiff;
            }
        }
    }

    private void Zoom(float zoomDiff) {
        if (zoomDiff != 0) {
            transform.position = transform.position + (transform.forward * zoomDiff);
        }
    }
}
