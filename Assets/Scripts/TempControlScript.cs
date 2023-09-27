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

    private float temperatureDifference = 1;

    float GetDeltaTemperature(float currentTemperature, float height) {
        float sign = 0;
        if (equilibriumTemperature < currentTemperature) sign = -1f;
        else if (equilibriumTemperature > currentTemperature) sign = 1f;
        float hFactor = 0;
        if (height < 2.5f) hFactor = 0;
        else if (height < 3.5f) hFactor = 0.10f * System.Convert.ToSingle(Mathf.Abs(equilibriumTemperature - currentTemperature) > temperatureDifference * 0.9f);
        else if (height < 4.5f) hFactor = 0.15f * System.Convert.ToSingle(Mathf.Abs(equilibriumTemperature - currentTemperature) > temperatureDifference * 0.6f);
        else if (height < 5.5f) hFactor = 0.25f * System.Convert.ToSingle(Mathf.Abs(equilibriumTemperature - currentTemperature) > temperatureDifference * 0.3f);
        else hFactor = 0.4f * System.Convert.ToSingle(equilibriumTemperature != currentTemperature);
        return sign * hFactor;
    }
    
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
            // ParticleScript particleScript = collider.gameObject.GetComponent<ParticleScript>();
            // if (equilibriumTemperature < particleScript.temperature) {
            //     particleScript.ChangeTemperature(-0.5f * Time.deltaTime);
            // } else if (equilibriumTemperature > particleScript.temperature) {
            //     particleScript.ChangeTemperature(0.5f * Time.deltaTime);
            // }
            ParticleScript particleScript = collider.gameObject.GetComponent<ParticleScript>();
            float deltaTemperature = GetDeltaTemperature(particleScript.temperature, particleScript.gameObject.transform.position.y);
            // if (particleScript.gameObject.name == "Particle (51)" && particleScript.gameObject.transform.position.y > 3.5f && particleScript.gameObject.transform.position.y < 4.5f && deltaTemperature != 0) Debug.Log($"{particleScript.temperature} {deltaTemperature} {particleScript.gameObject.transform.position.y}");
            particleScript.ChangeTemperature(deltaTemperature * Time.deltaTime);
        }
    }

    void Update() {
        temperatureDifference = Mathf.Max(1f, GameObject.Find("Water").GetComponent<WaterLayersManagerScript>().TemperatureDifference());
    }

}
