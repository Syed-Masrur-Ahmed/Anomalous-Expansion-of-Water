using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class WaterLayerScript : MonoBehaviour
{   
    public float avgTemperature;
    public GameObject Ice;
    
    void Update() {
        avgTemperature = gameObject.GetComponentsInChildren<ParticleScript>().Select((x) => x.temperature).Sum() / 25;
        TMP_Text temperatureText = gameObject.GetComponentInChildren<TMP_Text>();
        temperatureText.text = avgTemperature.ToString("0.0") + "° C";
        float colorRGChannel = 50 * (avgTemperature - 1) / 3f;
        if (avgTemperature < 0) return;
        if (avgTemperature < 1) 
        {
            Ice.GetComponent<Renderer>().material.color = new Color(219 / 255f, 247 / 255f, 248 / 255f, 1 - avgTemperature);
        }
        else
        {
            GetComponent<Renderer>().material.color = new Color(colorRGChannel / 255, colorRGChannel / 255, 1, 200 / 255f);
        }
    }
    
}
