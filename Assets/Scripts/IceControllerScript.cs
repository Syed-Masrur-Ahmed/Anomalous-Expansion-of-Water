using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceControllerScript : MonoBehaviour
{

    public Material topMat;
    public Material middleMat;
    public Material bottomMat;

    private WaterLayerScript[] waterLayerScripts;

    // Update is called once per frame
    void Update() {
        waterLayerScripts = gameObject.GetComponent<WaterLayersManagerScript>().waterLayerScripts;
        float floatTop = 0.7f + waterLayerScripts[4].avgTemperature;
        float floatMiddle = 0.7f + waterLayerScripts[3].avgTemperature;
        float floatBottom = 0.7f + waterLayerScripts[2].avgTemperature;
        if (floatTop > floatMiddle) (floatTop, floatMiddle) = (floatMiddle, floatTop);
        if (floatMiddle > floatBottom) (floatMiddle, floatBottom) = (floatBottom, floatMiddle);
        if (floatTop > floatMiddle) (floatTop, floatMiddle) = (floatMiddle, floatTop);
        topMat.SetFloat("_Float", floatTop);
        middleMat.SetFloat("_Float", floatMiddle);
        bottomMat.SetFloat("_Float", floatBottom);
    }
}
