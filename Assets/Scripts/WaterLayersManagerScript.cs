using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaterLayersManagerScript : MonoBehaviour
{

    public WaterLayerScript[] waterLayerScripts;

    public float TemperatureDifference() {
        return Mathf.Abs(waterLayerScripts[4].avgTemperature - waterLayerScripts[0].avgTemperature);
    }

    // Start is called before the first frame update
    void Start() {
        waterLayerScripts = GetComponentsInChildren<WaterLayerScript>();
    }

    // Update is called once per frame
    void Update() {
        Array.Sort(waterLayerScripts, (WaterLayerScript layer1, WaterLayerScript layer2) => {
            if (layer1.gameObject.transform.position.y < layer2.gameObject.transform.position.y) return -1;
            else if (layer1.gameObject.transform.position.y > layer2.gameObject.transform.position.y) return 1;
            return 0;
        });
    }
}
