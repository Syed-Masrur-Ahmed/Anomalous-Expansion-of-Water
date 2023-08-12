using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMaskScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }

}
