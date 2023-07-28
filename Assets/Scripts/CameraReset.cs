using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraReset : MonoBehaviour
{
    public GameObject cam;
    public void CamReset()
    {
        cam.transform.position = new Vector3(-1.0f, 0.0f, 0.0f);
        cam.transform.rotation = Quaternion.Euler (new Vector3(0.0f, 25.0f, 0.0f));
        Camera.main.orthographicSize = 5;
    }
}
