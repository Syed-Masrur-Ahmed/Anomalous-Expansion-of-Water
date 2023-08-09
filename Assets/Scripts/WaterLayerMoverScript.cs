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
        if (simulationCount < 100) return;
        simulationCount = 0;
        Array.Sort(waterLayerScripts, (WaterLayerScript layer1, WaterLayerScript layer2) => {
            if (layer1.gameObject.transform.position.y < layer2.gameObject.transform.position.y) return -1;
            else if (layer1.gameObject.transform.position.y > layer2.gameObject.transform.position.y) return 1;
            return 0;
        });
        int swap = -1;
        for (int i = 4; i > 0; i--) {
            if (waterLayerScripts[i - 1].avgTemperature - waterLayerScripts[i].avgTemperature >= 0.1f) swap = i;
        }
        if (swap == -1) return;
        StartCoroutine(SwapLayers(waterLayerScripts[swap].gameObject.transform, waterLayerScripts[swap - 1].gameObject.transform));
    }

    IEnumerator SwapLayers(Transform t1, Transform t2) {
        Vector3 pos1 = t1.position;
        Vector3 pos2 = t2.position;
        Vector3 center = (pos1 + pos2) * 0.5f;
        float progress = 0;
        while (progress < 1f) {
            progress += Time.deltaTime / swapDuration;
            t1.position = Vector3.Slerp(pos1 - center, pos2 - center, progress);
            t1.position += center;
            t2.position = Vector3.Lerp(pos2, pos1, progress);
            yield return null;
        }
        t1.position = pos2;
        t2.position = pos1;
    }

    void FixedUpdate() {
        simulationCount++;
    }

}
