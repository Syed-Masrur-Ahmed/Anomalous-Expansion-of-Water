using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TempControlScript : MonoBehaviour
{
    public TMP_Text CurrentTemp;
    public Button IncreaseTempButton;
    public Button DecreaseTempButton;
    public float equilibriumTemperature = 10;
    
    public void IncreaseTemp() {
        equilibriumTemperature += 1;
        CurrentTemp.text = equilibriumTemperature.ToString() + "° C";
        IncreaseTempButton.interactable = Convert.ToBoolean(10 - equilibriumTemperature);
        DecreaseTempButton.interactable = true;
    }

    public void DecreaseTemp() {
        equilibriumTemperature -= 1;
        CurrentTemp.text = equilibriumTemperature.ToString() + "° C";
        DecreaseTempButton.interactable = Convert.ToBoolean(-10 - equilibriumTemperature);
        IncreaseTempButton.interactable = true;
    }

    public void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag == "Particle") {
            ParticleScript particleScript = collider.gameObject.GetComponent<ParticleScript>();
            if (equilibriumTemperature < particleScript.temperature) {
                particleScript.ChangeTemperature(-0.5f * Time.deltaTime);
            } else if (equilibriumTemperature > particleScript.temperature) {
                particleScript.ChangeTemperature(0.5f * Time.deltaTime);
            }
        }
    }
}
