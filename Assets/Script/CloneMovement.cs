using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMovement : MonoBehaviour
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
    private PlayerControl playerControl;
    private CircleCollider2D circleCollider;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float minimumSpeed = 0.0f;
    [SerializeField] private float upSpeed = 0.5f;
    [SerializeField] private float slowSpeed = 0.0f;
    [SerializeField] private float facing = 0.5f;
    [SerializeField] private float turning = 0.5f;
    [SerializeField] private float minimumSpeedForBounce = 5.0f;
    [SerializeField] private float maxBounceTime = 1.0f;
    [SerializeField] private Player player;
    [SerializeField] private GameObject playerObject;


    public float brushRadius;
    private GridManager gridManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControl = playerObject.GetComponent<PlayerControl>();
        speed = playerControl.maxSpeed;
        turning = playerControl.turning;
        maxBounceTime = playerControl.maxBounceTime;
        circleCollider = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        brushRadius = transform.localScale.x;

    }

    void FixedUpdate()
    {
        lastVelocity = rb.velocity;


        if (!isBouncing)
        {
            Moving();
        }

        if (isBouncing)
        {
            if (speed > 0.0f)
                if (speed > 0.0f)
                {
                    
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

        
        if (gridManager != null)
        {
            Vector3 playerPosition = transform.position;
            Vector2Int gridPosition = gridManager.WorldToGridPosition(transform.position);
            gridManager.ChangeTileOwner(gridPosition, player, brushRadius);
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (speed > minimumSpeedForBounce && !isHittingWall)
        {
            circleCollider.isTrigger = true;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            circleCollider.isTrigger = false;
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
