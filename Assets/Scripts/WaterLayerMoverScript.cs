using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaterLayerMoverScript : MonoBehaviour
{
    private const float swapDuration = 1f;
    private WaterLayerScript[] waterLayerScripts;

    float GetDensity(float temperature) {
        temperature = Mathf.Round(temperature * 10f) * 0.1f;
        return -Mathf.Abs(temperature - 4);
    }

    (int i1, int i2) GetMovingLayers1() {
        Array.Sort(waterLayerScripts, (WaterLayerScript layer1, WaterLayerScript layer2) => {
            if (layer1.gameObject.transform.position.y < layer2.gameObject.transform.position.y) return -1;
            else if (layer1.gameObject.transform.position.y > layer2.gameObject.transform.position.y) return 1;
            return 0;
        });
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

    (int i1, int i2) GetMovingLayers2() {
        return (-1, -1);
    }

    void Start() {
        waterLayerScripts = GetComponentsInChildren<WaterLayerScript>();
        StartCoroutine(MoveLayersManager());
    }

    IEnumerator MoveLayersManager() {
        while (true) {
            var movingLayers = GetMovingLayers1();
            if (movingLayers.i1 != -1) yield return StartCoroutine(MoveLayers(
                waterLayerScripts[movingLayers.i1].gameObject.transform,
                waterLayerScripts[movingLayers.i2].gameObject.transform
            ));
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator MoveLayers(Transform t1, Transform t2) {
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
