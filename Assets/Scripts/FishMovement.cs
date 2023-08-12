using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 1f;
    
    private Vector3 GetNewVelocity() 
    {
        float currentPosX = transform.position.x;
        Vector3 newDirection = new Vector3(Random.Range(-1f, 1f), 0, 0);
        if (currentPosX < 0.5 || currentPosX > 4.5) newDirection.x = 2.5f - currentPosX;
        if (newDirection.x > 0) transform.eulerAngles = new Vector3(0, 90, 0);
        if (newDirection.x < 0) transform.eulerAngles = new Vector3(0, 270, 0);
        return (newDirection / newDirection.magnitude) * speed;
    }

    void FixedUpdate() 
    {
        if (Random.Range(0f, 1f) < 0.02f) rb.velocity = GetNewVelocity();
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
}
