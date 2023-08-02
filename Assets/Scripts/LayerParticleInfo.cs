using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LayerParticleInfo : MonoBehaviour
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
        return temperatureSum / 25;
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
        particlesInfo = particleScripts.Select((x) => new ParticleInfo(x.gameObject.transform.position, x.temperature)).ToArray();
        temperatureSum = particlesInfo.Select((x) => x.temperature).Sum();
        maxTemperature = particlesInfo.Select((x) => x.temperature).Max();
        minTemperature = particlesInfo.Select((x) => x.temperature).Min();
    }
}
