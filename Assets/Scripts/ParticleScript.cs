using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    private Rigidbody rb;
    private float speed = 0.3f;
    public float temperature = 0f;
    public ParticleInfo info;
    private GlobalParticleInfo globalInfo;


    private Vector3 GetNewVelocity() {
        float avgTemperature = globalInfo.GetAvgTemperature();
        float temperatureDifference = globalInfo.GetMaxTemperature() - globalInfo.GetMinTemperature();
        float tendToHeight = 4 + 0.125f * (temperature - avgTemperature);
        float heightLowerBound = -1f;
        float heightUpperBound = 1f;
        if (tendToHeight > transform.position.y) {
            heightLowerBound /= Mathf.Max(1, 0.01f * temperatureDifference * (tendToHeight - transform.position.y));
        } else if (tendToHeight < transform.position.y) {
            heightUpperBound /= Mathf.Max(1, 0.1f * temperatureDifference * (transform.position.y - tendToHeight));
        }
        Vector3 newDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(heightLowerBound, heightUpperBound), Random.Range(-1f, 1f));
        return (newDirection / newDirection.magnitude) * speed;
    }

    public void ChangeTemperature(float delT) {
        if (temperature + delT >= 90f) {
            temperature = 90f;
            return;
        }
        temperature += delT;
        float colorGBChannel = 1 - (temperature / 90);
        GetComponent<Renderer>().material.SetColor("_Color", new Color(1, colorGBChannel, colorGBChannel));
        speed += (delT / 0.1f) * 0.002f;
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        info = new ParticleInfo(transform.position, temperature);
        globalInfo = GetComponentInParent<GlobalParticleInfo>();
    }

    void FixedUpdate() {
        info.position = transform.position;
        info.temperature = temperature;
        if (Random.Range(0f, 1f) < 0.05f) rb.velocity = GetNewVelocity();
    }

    void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag != "Particle") return;
        float otherTemperature = collider.gameObject.GetComponent<ParticleScript>().temperature;
        float temperatureDifference = otherTemperature - temperature;
        ChangeTemperature(temperatureDifference / 20);
    }
}
