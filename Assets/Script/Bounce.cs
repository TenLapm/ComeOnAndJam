using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector3 lastVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocity.magnitude;
        var directiob = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = directiob * Mathf.Max(speed, 0f);
    }
}
