using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    Rigidbody rb;
    float speed = 0.3f;

    Vector3 GetNewVelocity()
    {
        Vector3 newDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        return (newDirection / newDirection.magnitude) * speed;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // if (gameObject.name == "Particle (113)") Debug.Log(rb.velocity);
        if (Random.Range(0f, 1f) < 0.05f) rb.velocity = GetNewVelocity();
    }
}
