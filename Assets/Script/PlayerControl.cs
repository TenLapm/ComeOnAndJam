using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player
{
    PlayerA, PlayerB, Default
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
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float minimumSpeed = 0.0f;
    [SerializeField] private float upSpeed = 0.5f;
    [SerializeField] private float slowSpeed = 0.5f;
    [SerializeField] private float facing = 0.5f;
    [SerializeField] private float minimumSpeedForBounce = 5.0f;
    [SerializeField] private float maxBounceTime = 1.0f;
    [SerializeField] private Player player;

    [SerializeField] private GameObject trailSpawnPoint1;
    [SerializeField] private GameObject trailSpawnPoint2;
    [SerializeField] private GameObject trailSpawnPoint3;
    public float brushRadius;
    private GridManager gridManager;

    [SerializeField] private GameObject spritePrefab1;
    [SerializeField] private GameObject spritePrefab2;
    [SerializeField] private GameObject spritePrefab3;

    [SerializeField] private float spawnDistance = 5.0f; // Distance threshold for spawning sprites
    private Vector3 lastSpawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager == null)
        {
            Debug.LogError("GridManager component not found in the scene.");
        }
        brushRadius = transform.localScale.x;
        lastSpawnPosition = transform.position;
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
            {
                speed -= slowSpeed;
            }
            bounceTime -= Time.deltaTime;
            Moving();
        }

        if (bounceTime < 0.0f)
        {
            isBouncing = false;
        }

        if (speed < 0.0f)
        {
            speed = 0.0f;
        }

        if (gridManager != null)
        {
            Vector3 playerPosition = transform.position;
            Vector2Int gridPosition = gridManager.WorldToGridPosition(transform.position);
            gridManager.ChangeTileOwner(gridPosition, player, brushRadius);
        }

        if (Vector3.Distance(transform.position, lastSpawnPosition) >= spawnDistance)
        {
            SpawnSprite();
            lastSpawnPosition = transform.position;
        }
    }

    private void Control()
    {
        if (player == Player.PlayerA)
        {
            if (Input.GetKey(KeyCode.W))
            {
                speed = Mathf.Min(speed + upSpeed, maxSpeed);
                isDeacceleration = false;
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                isDeacceleration = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                speed = Mathf.Max(speed - slowSpeed, minimumSpeed);
                isDeacceleration = false;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                isDeacceleration = true;
            }
            if (isDeacceleration)
            {
                speed = Mathf.Max(speed - (slowSpeed - 0.03f), minimumSpeed);
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
        else if (player == Player.PlayerB)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                speed = Mathf.Min(speed + upSpeed, maxSpeed);
                isDeacceleration = false;
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                isDeacceleration = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                speed = Mathf.Max(speed - slowSpeed, minimumSpeed);
                isDeacceleration = false;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                isDeacceleration = true;
            }
            if (isDeacceleration)
            {
                speed = Mathf.Max(speed - (slowSpeed - 0.03f), minimumSpeed);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.transform.Rotate(0.0f, 0.0f, facing);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.transform.Rotate(0.0f, 0.0f, -facing);
            }
        }
    }

    private void Moving()
    {
        if (!isBouncing)
        {
            rb.velocity = transform.up * speed;
        }
        else if (isBouncing)
        {
            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (speed > minimumSpeedForBounce)
        {
            direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotationZ = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = rotationZ;
            isBouncing = true;
            bounceTime = maxBounceTime;
        }
    }

    private void SpawnSprite()
    {
        GameObject selectedPrefab = GetRandomSpritePrefab();
        Vector3 spawnPoint = GetRandomSpawnPoint();
        Instantiate(selectedPrefab, spawnPoint, Quaternion.identity);
    }

    private GameObject GetRandomSpritePrefab()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                return spritePrefab1;
            case 1:
                return spritePrefab2;
            case 2:
                return spritePrefab3;
            default:
                return spritePrefab1; // Fallback to the first prefab
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                return trailSpawnPoint1.transform.position;
            case 1:
                return trailSpawnPoint2.transform.position;
            case 2:
                return trailSpawnPoint3.transform.position;
            default:
                return trailSpawnPoint1.transform.position; // Fallback to the first spawn point
        }
    }
}
