using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber;
    public float moveSpeed = 5f;
    public Color playerColor;
    private GridManager gridManager;
    private Vector2Int previousGridPosition;
    private float playerRadius;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();

        // Calculate player radius based on sprite size
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float spriteWidth = spriteRenderer.bounds.size.x;
            float spriteHeight = spriteRenderer.bounds.size.y;
            playerRadius = Mathf.Max(spriteWidth, spriteHeight) / 2f;
        }
        else
        {
            playerRadius = 1.0f; // Default radius if no sprite renderer found
        }

        Vector2Int gridPosition = gridManager.WorldToGridPosition(transform.position);
        previousGridPosition = gridPosition;
        gridManager.ChangeTileColor(gridPosition, playerColor, playerRadius);
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(moveX, moveY, 0));

        Vector2Int currentGridPosition = gridManager.WorldToGridPosition(transform.position);
        if (currentGridPosition != previousGridPosition)
        {
            gridManager.ChangeTileColor(currentGridPosition, playerColor, playerRadius);
            previousGridPosition = currentGridPosition;
        }
    }
}
