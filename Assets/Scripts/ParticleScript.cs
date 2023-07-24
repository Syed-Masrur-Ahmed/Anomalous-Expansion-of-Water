using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    private Rigidbody rb;
    private float speed = 0.3f;
    
    public float temperature = 0f;

    private Vector3 GetNewVelocity()
    {
        Vector3 newDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        return (newDirection / newDirection.magnitude) * speed;
    }

    public void ChangeTemperature(float delT)
    {
        if (temperature >= 90f) return;
        temperature += delT;
        float colorGBChannel = 1 - (temperature / 90);
        GetComponent<Renderer>().material.SetColor("_Color", new Color(1, colorGBChannel, colorGBChannel));
        speed += (delT / 0.1f) * 0.002f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetKeyDown("space")) {
            Debug.Log($"{gameObject.name} : {temperature}");
        }
    }

    void FixedUpdate()
    {
        if (Random.Range(0f, 1f) < 0.05f) rb.velocity = GetNewVelocity();
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.collider.gameObject.tag != "Particle") return;
        float otherTemperature = collisionInfo.collider.gameObject.GetComponent<ParticleScript>().temperature;
        ChangeTemperature((otherTemperature - temperature) / 8);
    }
}
