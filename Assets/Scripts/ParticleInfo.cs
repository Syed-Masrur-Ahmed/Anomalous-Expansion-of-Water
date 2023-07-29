using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInfo 
{
    public Vector3 position;
    public float temperature;

    public ParticleInfo(Vector3 _position, float _temperature) {
        position = _position;
        temperature = _temperature;
    }

}
