using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private bool isBouncing = false;
    private bool isDeAcceleration = false;
    private float speed = 0.0f;
    private Rigidbody2D rb;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float minimumSpeed = 0.0f;
    [SerializeField] private float upSpeed = 0.5f;
    [SerializeField] private float slowSpeed = 0.5f;
    [SerializeField] private float facing = 0.5f;
    [SerializeField] private float minimumSpeedBounce = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Control();
        Moving();
    }

    private void Control()
    {
        if(Input.GetKey(KeyCode.W)) {

            if(speed > maxSpeed)
            {
                speed = maxSpeed;
            }

            if(speed != maxSpeed)
            {
                speed += upSpeed;
            }
            isDeAcceleration = false;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            isDeAcceleration = true;
        }
        
        else if(Input.GetKey(KeyCode.S)) 
        {
            if (speed < minimumSpeed)
            {
                speed = minimumSpeed;
            }

            if (speed != minimumSpeed)
            {
                speed -= slowSpeed;
            }
            isDeAcceleration = false;
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            isDeAcceleration = true;
        }

        if (isDeAcceleration == true)
        {
            if (speed < minimumSpeed)
            {
                speed = minimumSpeed;
            }

            if (speed != minimumSpeed)
            {
                speed -= (slowSpeed - 0.03f);
            }

        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Rotate(0.0f, 0.0f, facing);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Rotate(0.0f, 0.0f, -facing);
        }

        
    }

    private void Moving()
    {
        //rb.MovePosition(rb.position + (Vector2)transform.forward * speed * Time.deltaTime);
        rb.velocity = transform.up * speed;
    }
}
