using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 1f;
    
    private Vector3 GetNewVelocity() {
        Vector3 newDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        if (newDirection.x > 0) transform.eulerAngles = new Vector3(0, 0, 0);
        if (newDirection.x < 0) transform.eulerAngles = new Vector3(0, 180, 0);
        return (newDirection / newDirection.magnitude) * speed;
    }

    void FixedUpdate() {
        if (Random.Range(0f, 1f) < 0.01f) rb.velocity = GetNewVelocity();
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
}
