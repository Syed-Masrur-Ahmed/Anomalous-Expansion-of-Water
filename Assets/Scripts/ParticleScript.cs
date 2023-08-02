using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    private Rigidbody rb;
    private float speed = 0.72f;
    public float temperature = 25f;


    private Vector3 GetNewVelocity() {
        Vector3 newDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        return (newDirection / newDirection.magnitude) * speed;
    }

    public void ChangeTemperature(float delT) {
        temperature += delT;
        if (temperature >= 90) temperature = 90;
        GetComponent<Renderer>().material.SetColor("_Color", GetColor());
        speed = 0.012f*temperature + 0.42f;
    }

    public Color GetColor() {
        if (temperature < 25) {
            float colorRGChannel = (temperature + 10) / 35f;
            return new Color(colorRGChannel, colorRGChannel, 1);
        } else {
            float colorGBChannel = (90 - temperature) / 65f;
            return new Color(1, colorGBChannel, colorGBChannel);
        }
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (Random.Range(0f, 1f) < 0.05f) rb.velocity = GetNewVelocity();
    }

    void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag != "Particle") return;
        float otherTemperature = collider.gameObject.GetComponent<ParticleScript>().temperature;
        float temperatureDifference = otherTemperature - temperature;
        ChangeTemperature(temperatureDifference / 100);
    }
}
