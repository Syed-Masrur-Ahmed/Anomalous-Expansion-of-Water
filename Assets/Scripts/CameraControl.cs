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
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            float verticalInput = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            var mainCamY = Camera.main.transform.rotation.x;
            if ((verticalInput < 0 && mainCamY < 0.70) || (verticalInput > 0 && mainCamY > 0.04))
            {
                transform.Rotate(Vector3.right, -verticalInput);
            }
            /*transform.Rotate(Vector3.up, horizontalInput, Space.World);*/
        }
    }

    private void Pan() {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
            Vector3 check = transform.position - transform.right * Input.GetAxis("Mouse X") - transform.up * Input.GetAxis("Mouse Y");
            if (check.x > -10 && check.x < 10 && check.y > -20 && check.y < 10) {
                transform.position = check;
            }
        }
    }

    private void Zoom(float zoomDiff) {
        if (zoomDiff != 0) {
            var check = transform.position + transform.forward * zoomDiff;
            var mainCamY = Camera.main.transform.rotation.x;
            if ((check.z > 10 && check.z < 25) || (check.z<15 && check.z>-5))
            {
                transform.position = transform.position + (transform.forward * zoomDiff);
            }
        }
    }
}
