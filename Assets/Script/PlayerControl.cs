using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public enum Player
{
    PlayerA, PlayerB
}

public class PlayerControl : MonoBehaviour
{
    private bool isBouncing = false;
    private bool isDeacceleration = false;
    private float speed = 0.0f;
    private Vector3 lastVelocity;
    private Rigidbody2D rb;
    private float bounceTime;
    private Vector3 direction;
    private float directionF;
    private float turnDirection;
    private float headingDirection;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float minimumSpeed = 0.0f;
    [SerializeField] private float upSpeed = 0.5f;
    [SerializeField] private float slowSpeed = 0.5f;
    [SerializeField] private float facing = 0.5f;
    [SerializeField] private float minimumSpeedForBounce = 5.0f;
    [SerializeField] private float maxBounceTime = 1.0f;
    [SerializeField] private Player player;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!isBouncing)
        {
            Control();
        }
    }

    
    void Update()
    {
        lastVelocity = rb.velocity;
        
        if (!isBouncing)
        {
            
            Moving();
        }

        if (isBouncing)
        {
            if(speed > 0.0f)
            {
                speed -= slowSpeed;
            }
            bounceTime -= Time.deltaTime;
            Moving();
        }
        
        if(bounceTime < 0.0f)
        {
            isBouncing = false;
        }

        if(speed < 0.0f)
        {
            speed = 0.0f;
        }
    }

    private void Control()
    {
        if(player == Player.PlayerA)
        {
            if (Input.GetKey(KeyCode.W))
            {

                if (speed > maxSpeed)
                {
                    speed = maxSpeed;
                }

                if (speed != maxSpeed)
                {
                    speed += upSpeed * Time.deltaTime;
                }
                isDeacceleration = false;
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                isDeacceleration = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (speed < minimumSpeed)
                {
                    speed = minimumSpeed;
                }

                if (speed != minimumSpeed)
                {
                    speed -= slowSpeed;
                }
                isDeacceleration = false;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                isDeacceleration = true;
            }
            if (isDeacceleration == true)
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
            
        }
        if(player == Player.PlayerB)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {

                if (speed > maxSpeed)
                {
                    speed = maxSpeed;
                }

                if (speed != maxSpeed)
                {
                    speed += upSpeed * Time.deltaTime;
                }
                isDeacceleration = false;
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                isDeacceleration = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (speed < minimumSpeed)
                {
                    speed = minimumSpeed;
                }

                if (speed != minimumSpeed)
                {
                    speed -= slowSpeed;
                }
                isDeacceleration = false;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                isDeacceleration = true;
            }
            if (isDeacceleration == true)
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
            
        }
    }

    private void Moving()
    {
        //rb.MovePosition(rb.position + (Vector2)transform.forward * speed * Time.deltaTime);
        if (!isBouncing)
        {
            turnDirection = - Input.GetAxis("Horizontal");
            if(isDeacceleration)
            {
                headingDirection = Input.GetAxis("Vertical") * slowSpeed;
            }
            else
            {
                headingDirection = Input.GetAxis("Vertical") * speed;
            }
            
            directionF = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
            rb.rotation += turnDirection * facing * rb.velocity.magnitude * directionF;

            rb.AddRelativeForce(Vector2.up * headingDirection);
            rb.AddRelativeForce(-Vector2.right * rb.velocity.magnitude * turnDirection / 2);
        }

        else if (isBouncing)
        {

            rb.velocity = direction * Mathf.Max(speed, 0f);
            
        }
        
    }

    //private void Bouceing(Collision2D collision)
    //{

    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (speed > minimumSpeedForBounce)
        {
            direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.velocity = direction * speed;
            
            Quaternion rotationZ = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = rotationZ;
            isBouncing = true;
            bounceTime = maxBounceTime;
        }
        else
        {
            rb.velocity = direction;
        }
    }
}
