using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAnimation : MonoBehaviour
{
    private GameObject IceMask;
    public float temperature = 10f;

    void Start(){
        IceMask = GameObject.FindGameObjectWithTag("Ice Mask");
    }

    void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag == "Water Layer") {
            temperature = collider.gameObject.GetComponent<WaterLayerScript>().avgTemperature;
            if (temperature < 0) {
                IceMask.transform.position = new Vector3(2.5f, 2.4f * temperature + 6f, 2.25f);
                IceMask.GetComponent<Renderer>().enabled = temperature > -0.5f;
            } else {
                IceMask.transform.position = new Vector3(2.5f, 6f, 2.25f);
            }
        }
    }
}
