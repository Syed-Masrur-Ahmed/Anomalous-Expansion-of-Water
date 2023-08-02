using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class WaterLayerScript : MonoBehaviour
{   
    public float avgTemperature;
    
    void FixedUpdate() {
        avgTemperature = gameObject.GetComponentsInChildren<ParticleScript>().Select((x) => x.temperature).Sum() / 25;
        TMP_Text temperatureText = gameObject.GetComponentInChildren<TMP_Text>();
        temperatureText.text = avgTemperature.ToString("0.0") + "° C";
    }
    
}
