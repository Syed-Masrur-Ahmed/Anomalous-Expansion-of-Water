using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaterLayersSwapScript : MonoBehaviour
{
    private const float swapDuration = 1f;
    private WaterLayerScript[] waterLayerScripts;

    float GetDensity(float temperature) {
        temperature = Mathf.Round(temperature * 10f) * 0.1f;
        return -Mathf.Abs(temperature - 4);
    }

    (int i1, int i2) GetMisplacedLayers() {
        waterLayerScripts = gameObject.GetComponent<WaterLayersManagerScript>().waterLayerScripts;
        int i1 = -1;
        float maxDifference = 0;
        for (int i = 1; i < 5; i++) {
            float d1 = GetDensity(waterLayerScripts[i].avgTemperature);
            float d2 = GetDensity(waterLayerScripts[i - 1].avgTemperature);
            if (d1 - d2 > maxDifference) {
                i1 = i;
                maxDifference = d1 - d2;
            }
        }
        return (i1, i1 - 1);
    }

    void Start() {
        StartCoroutine(SwapLayersManager());
    }

    IEnumerator SwapLayersManager() {
        while (true) {
            yield return new WaitForSeconds(0.5f); // has to stay above GetMisplacedLayers because Start of this script runs before WaterLayersManagerScript's Start
            var movingLayers = GetMisplacedLayers();
            if (movingLayers.i1 != -1) yield return StartCoroutine(SwapLayers(
                waterLayerScripts[movingLayers.i1].gameObject.transform,
                waterLayerScripts[movingLayers.i2].gameObject.transform
            ));
        }
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

}
