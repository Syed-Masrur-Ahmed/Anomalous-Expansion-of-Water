using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaterLayerMoverScript : MonoBehaviour
{
    private const float swapDuration = 1f;
    private int simulationCount;
    private WaterLayerScript[] waterLayerScripts;

    void Start() {
        simulationCount = 0;
        waterLayerScripts = GetComponentsInChildren<WaterLayerScript>();
    }

    void Update() {
        if (simulationCount < 250) return;
        simulationCount = 0;
        for (int i = 0; i < 5; i++) {
            Debug.Log(waterLayerScripts[i].avgTemperature);
        }
        Array.Sort(waterLayerScripts, (WaterLayerScript layer1, WaterLayerScript layer2) => {
            if (layer1.gameObject.transform.position.y < layer2.gameObject.transform.position.y) return -1;
            else if (layer1.gameObject.transform.position.y > layer2.gameObject.transform.position.y) return 1;
            return 0;
        });
        float maxTempDiff = 0; // change to density for anomaly
        int swap = -1;
        for (int i = 1; i < 5; i++) {
            if (waterLayerScripts[i - 1].avgTemperature - waterLayerScripts[i].avgTemperature > maxTempDiff) {
                swap = i;
                maxTempDiff = waterLayerScripts[i - 1].avgTemperature - waterLayerScripts[i].avgTemperature;
            }
        }
        if (maxTempDiff < 0.1f) return;
        waterLayerScripts[swap].gameObject.transform.position += Vector3.down;
        waterLayerScripts[swap - 1].gameObject.transform.position += Vector3.up;
    }

    // IEnumerator SwapLayers(WaterLayerScript layer1, WaterLayerScript layer2) {

    // }

    void FixedUpdate() {
        simulationCount++;
    }

}
