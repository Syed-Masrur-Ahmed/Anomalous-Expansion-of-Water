using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeaterScript : MonoBehaviour
{
    public TMP_Text OnOffText;
    public bool isOn = false;

    void OnMouseDown() {
        isOn = !isOn;
        if (isOn) {
            OnOffText.text = "On";
        } else {
            OnOffText.text = "Off";
        }
    }

    void OnTriggerStay(Collider collider) {
        GameObject particle = collider.gameObject;
        if (isOn && particle.tag == "Particle") particle.GetComponent<ParticleScript>().ChangeTemperature(0.1f);
    }
}
