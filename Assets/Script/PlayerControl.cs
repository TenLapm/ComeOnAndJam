using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public enum Player
{
    PlayerA, PlayerB,Default
}

public class PlayerControl : MonoBehaviour
{
    private bool isBouncing = false;
    private bool isDeacceleration = false;
    private bool isHittingWall = false;
    private float speed = 0.0f;
    private Vector3 lastVelocity;
    private Rigidbody2D rb;
    private float bounceTime;
    private Vector3 direction;
    private float spiningSpeed;
    public float maxSpeed = 10.0f;
    [SerializeField] private float minimumSpeed = 0.0f;
    [SerializeField] private float upSpeed = 0.5f;
    [SerializeField] private float slowSpeed = 0.5f;
    public float turning = 0.5f;
    [SerializeField] private float minimumSpeedForBounce = 5.0f;
    public float maxBounceTime = 1.0f;
    [SerializeField] private Player player;

    public float brushRadius;
    private GridManager gridManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                isDeacceleration = true;
            }
            brushRadius = transform.localScale.x;

    }

    void FixedUpdate()
    {
        lastVelocity = rb.velocity;


        if (!isBouncing)
        {
            Control();
            Moving();
        }

        if (isBouncing)
        {
            if (speed > 0.0f)
                if (speed > 0.0f)
                {
                    speed -= slowSpeed;
                }
            bounceTime -= Time.deltaTime;
            gameObject.transform.Rotate(0.0f, 0.0f, -spiningSpeed);
            Moving();

        }

        if (bounceTime < 0.0f && isBouncing)
        {
            isBouncing = false;
            isHittingWall = false;
        }

        if (speed < 0.0f)
        {
            if (bounceTime < 0.0f)
            {
                speed = 0.0f;
                isBouncing = false;
            }
        }
        if (gridManager != null)
        {
            Vector3 playerPosition = transform.position;
            Vector2Int gridPosition = gridManager.WorldToGridPosition(transform.position);
            gridManager.ChangeTileOwner(gridPosition, player, brushRadius);
        }
    }

    private void Control()
    {
        if (player == Player.PlayerA)
        {
            if (Input.GetKey(KeyCode.W))
            {

                if (speed > maxSpeed)
                {
                    speed = maxSpeed;
                }

                if (speed != maxSpeed)
                {
                    speed += upSpeed;
                }
                isDeacceleration = false;
            }
            
            if (Input.GetKeyUp(KeyCode.W))
                {
                    isDeacceleration = true;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    if (speed < minimumSpeed)
                        if (speed <= minimumSpeed)
                        {
                            speed = minimumSpeed;
                        }

                    if (speed != minimumSpeed)
                        if (speed <= 0)
                        {
                            speed -= slowSpeed * 5;
                        }

                        else if (speed != minimumSpeed)
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
            if (Input.GetKey(KeyCode.A))
            {
                
                gameObject.transform.Rotate(0.0f, 0.0f, turning);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                
                gameObject.transform.Rotate(0.0f, 0.0f, -turning);
            }
        }
        if (player == Player.PlayerB)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {

                if (speed > maxSpeed)
                {
                    speed = maxSpeed;
                }

                if (speed != maxSpeed)
                {
                    speed += upSpeed;
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
            if (Input.GetKey(KeyCode.LeftArrow))
            {
               
                gameObject.transform.Rotate(0.0f, 0.0f, turning);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                
                gameObject.transform.Rotate(0.0f, 0.0f, -turning);
            }
        }

        if (speed <= 0 && isDeacceleration)
        {
            speed = 0;
            isDeacceleration = false;
        }
    }

    private void Moving()
    {
        //rb.MovePosition(rb.position + (Vector2)transform.forward * speed * Time.deltaTime);
        if (!isBouncing)
        {
            rb.velocity = transform.up * speed;
            
            if (isHittingWall)
            {
                rb.velocity = transform.up * 1;
            }
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
        if (speed > minimumSpeedForBounce && !isHittingWall)
        {
            direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotationZ = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = rotationZ;
            spiningSpeed = (speed - minimumSpeedForBounce) / 0.5f;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Quaternion rotationZ = Quaternion.LookRotation(Vector3.forward, direction);
            //transform.rotation = rotationZ;
            isBouncing = true;
            bounceTime = maxBounceTime;
        }
        isDeacceleration = true;
        isHittingWall = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isBouncing)
        {
            speed = 0;
            isHittingWall = false;
        }

    }

    private Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        float gridWidth = gridManager.cols * gridManager.tileSize;
        float gridHeight = gridManager.rows * gridManager.tileSize;
        Vector3 startPos = new Vector3(-gridWidth / 2 + gridManager.tileSize / 2, -gridHeight / 2 + gridManager.tileSize / 2, 0);

        Vector3 localPos = worldPosition - startPos;
        int col = Mathf.FloorToInt(localPos.x / gridManager.tileSize);
        int row = Mathf.FloorToInt(localPos.y / gridManager.tileSize);

        return new Vector2Int(col, row);
    }
}
