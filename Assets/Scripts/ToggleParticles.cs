using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleParticles : MonoBehaviour
{
    private GameObject[] particleList;
    void Start(){
        particleList = GameObject.FindGameObjectsWithTag("Particle");
    }

    public void Toggle() {
        foreach(GameObject particle in particleList) {
            particle.GetComponent<Renderer>().enabled = !particle.GetComponent<Renderer>().enabled;
        }
    }
}
