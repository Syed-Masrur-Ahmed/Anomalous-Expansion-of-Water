using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlobalParticleInfo : MonoBehaviour
{

    private ParticleScript[] particleScripts;
    private int particleCount;
    private ParticleInfo[] particlesInfo;
    private float temperatureSum;
    private float maxTemperature;
    private float minTemperature;

    public ParticleInfo[] ExportInfo() {
        return particlesInfo;
    }

    public float GetAvgTemperature() {
        return temperatureSum / particleCount;
    }

    public float GetMaxTemperature() {
        return maxTemperature;
    }

    public float GetMinTemperature() {
        return minTemperature;
    }

    void Start() {
        particleScripts = GetComponentsInChildren<ParticleScript>();
        particleCount = particleScripts.Length;
    }
    
    void FixedUpdate() {
        particlesInfo = particleScripts.Select((x) => x.info).ToArray();
        foreach (ParticleInfo info in particlesInfo) {
            temperatureSum += info.temperature;
            maxTemperature = Mathf.Max(maxTemperature, info.temperature);
            minTemperature = Mathf.Min(minTemperature, info.temperature);
        }
    }
}
