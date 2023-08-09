using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseViewScript : MonoBehaviour
{
    public TMP_Text CurrentView;
    public GameObject Water;
    private string[] options = {"Default", "Particles", "Layers"};
    private GameObject[] particleList;
    private GameObject[] layerList;
    private int index = 0;

    void Start() {
        particleList = GameObject.FindGameObjectsWithTag("Particle");
        layerList = GameObject.FindGameObjectsWithTag("Water Layer");
    }
    
    public void ChooseNextView() 
    {
        ChangeView(1);
    }

    public void ChoosePreviousView() 
    {
        ChangeView(2);
    }

    private void ChangeView(int n)
    {
        index += n;
        CurrentView.text = options[index % 3];
        foreach(GameObject particle in particleList) {
            particle.GetComponent<Renderer>().enabled = index % 3 == 1;
        }
        foreach(GameObject layer in layerList) {
            layer.GetComponent<Renderer>().enabled = index % 3 == 2;
        }
        Water.GetComponent<Renderer>().enabled = !(index % 3 ==2);

    }  
}
