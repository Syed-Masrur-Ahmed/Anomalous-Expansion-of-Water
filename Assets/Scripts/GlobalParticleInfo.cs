using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlobalParticleInfo : MonoBehaviour
{

    private ParticleScript[] particleScripts;
    private ParticleInfo[] particlesInfo;
    private float temperatureSum;
    private float maxTemperature;
    private float minTemperature;

    public ParticleInfo[] ExportInfo() {
        return particlesInfo;
    }

    public float GetAvgTemperature() {
        return temperatureSum / 125;
    }

    public float GetMaxTemperature() {
        return maxTemperature;
    }

    public float GetMinTemperature() {
        return minTemperature;
    }

    void Start() {
        particleScripts = GetComponentsInChildren<ParticleScript>();
    }

    void Update() {
        if (Input.GetKeyDown("space")) {
            Debug.Log(GetAvgTemperature());
            Debug.Log(GetMaxTemperature());
            Debug.Log(GetMinTemperature());
        }
    }
    
    void FixedUpdate() {
        temperatureSum = 0f;
        particlesInfo = particleScripts.Select((x) => new ParticleInfo(x.gameObject.transform.position, x.temperature)).ToArray();
        foreach (ParticleInfo info in particlesInfo) {
            temperatureSum += info.temperature;
            maxTemperature = Mathf.Max(maxTemperature, info.temperature);
            minTemperature = Mathf.Min(minTemperature, info.temperature);
        }
    }
}
