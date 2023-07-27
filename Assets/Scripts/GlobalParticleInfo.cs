using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlobalParticleInfo : MonoBehaviour
{

    private ParticleScript[] particleScripts;
    private int particleCount;
    private float[] particleTemperatures;
    private float temperatureSum;
    private float maxTemperature;
    private float minTemperature;

    public float GetAvgTemperature() {
        return temperatureSum / particleCount;
    }

    public float GetMaxTemperature() {
        return maxTemperature;
    }

    public float GetMinTemperature() {
        return minTemperature;
    }

    // Start is called before the first frame update
    void Start() {
        particleScripts = GetComponentsInChildren<ParticleScript>();
        particleCount = particleScripts.Length;
    }

    // Update is called once per frame
    void Update() {
        particleTemperatures = particleScripts.Select((x) => x.temperature).ToArray();
        temperatureSum = particleTemperatures.Sum();
        maxTemperature = particleTemperatures.Max();
        minTemperature = particleTemperatures.Min();
        if (Input.GetKeyDown("space")) {
            Debug.Log(GetAvgTemperature());
        }
    }
}
