using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class WaterLayerScript : MonoBehaviour
{   
    public float avgTemperature;

    void Update() {
        avgTemperature = gameObject.GetComponentsInChildren<ParticleScript>().Select((x) => x.temperature).Sum() / 25;
        TMP_Text temperatureText = gameObject.GetComponentInChildren<TMP_Text>();
        temperatureText.text = avgTemperature.ToString("0.0") + "° C";
        float colorRGChannel = 15 * avgTemperature;
        if (avgTemperature < -0.5f) 
        {
            GetComponent<Renderer>().material.color = new Color(0, 0, 1, 200 / 255f);
            return;
        }
        if (avgTemperature < 0) 
        { 
            //GetComponent<Renderer>().material.color = new Color(0, 0, 1, 2 * avgTemperature + 1f);
            return;
        }
        GetComponent<Renderer>().material.color = new Color(colorRGChannel / 255, colorRGChannel / 255, 1, 200 / 255f);
    }
    
}
