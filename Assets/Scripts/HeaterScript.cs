using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeaterScript : MonoBehaviour
{
    public TMP_Text OnOffText;
    public bool isOn = false;
    void OnMouseDown(){
        isOn = !isOn;
        if(isOn){
            OnOffText.text = "On";
        }
        else{
            OnOffText.text = "Off";
        }
    }

    void OnTriggerStay(Collider collider){
        GameObject particle = collider.gameObject;
        if(isOn && particle.tag == "Particle"){
            particle.GetComponent<ParticleScript>().temperature += 0.1f;
            if(particle.GetComponent<ParticleScript>().temperature > 90){
                particle.GetComponent<ParticleScript>().temperature = 90;
            }
            float colorGBChannel = 1 - (particle.GetComponent<ParticleScript>().temperature/90);
            particle.GetComponent<Renderer>().material
            .SetColor("_Color", new Color(1, colorGBChannel, colorGBChannel));
            particle.GetComponent<ParticleScript>().speed += 0.002f;
        }
    }
}
