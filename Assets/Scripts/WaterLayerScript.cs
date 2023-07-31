using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaterLayerScript : MonoBehaviour
{   
    public float temperature;
    
    void FixedUpdate()
    {
        temperature = gameObject.GetComponentInChildren<GlobalParticleInfo>().GetAvgTemperature();
        TMP_Text temperatureText = gameObject.GetComponentInChildren<TMP_Text>();
        temperatureText.text = temperature.ToString("0.0") + "° C";
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Water Layer")
        {
            float otherTemperature = collider.gameObject.GetComponent<WaterLayerScript>().temperature;
            float otherHeight = collider.gameObject.transform.position.y;
            if (temperature > otherTemperature && transform.position.y < otherHeight)
            {
                //slowly move up 1 unit
            }
            if (temperature < otherTemperature && transform.position.y > otherHeight)
            {
                //slowly move down 1 unit
            }
        }
    }
    
}
