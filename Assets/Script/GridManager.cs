using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public int rows = 10;
    public int cols = 10;
    public float tileSize = 1.0f;
    public GameObject tilePrefab;
    public TextMeshProUGUI playerAText;
    public TextMeshProUGUI playerBText;

    private GameObject[,] gridArray;
    private List<GameObject> tilePool = new List<GameObject>();
    private Player[,] gridOwners;

    void Start()
    {
        gridOwners = new Player[rows, cols];
        GenerateGridWithPooling();
        InitializeGridOwners();
        UpdateGridOwnershipDisplay();
    }

    void GenerateGridWithPooling()
    {
        gridArray = new GameObject[rows, cols];

        for (int i = 0; i < rows * cols; i++)
        {
            GameObject tile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
            tile.SetActive(false);
            tilePool.Add(tile);
        }

        float gridWidth = cols * tileSize;
        float gridHeight = rows * tileSize;
        Vector3 startPos = new Vector3(-gridWidth / 2 + tileSize / 2, -gridHeight / 2 + tileSize / 2, 0);

        int poolIndex = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 pos = startPos + new Vector3(col * tileSize, row * tileSize, 0);
                GameObject tile = tilePool[poolIndex];
                tile.transform.position = pos;
                tile.SetActive(true);
                tile.name = $"Tile_{row}_{col}";
                gridArray[row, col] = tile;
                poolIndex++;
            }
        }
    }

    void InitializeGridOwners()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                gridOwners[row, col] = Player.Default;
            }
        }
    }

    public void ChangeTileOwner(Vector2Int gridPosition, Player player, float radius)
    {
        int radiusInTiles = Mathf.CeilToInt(radius / tileSize);

        for (int x = -radiusInTiles; x <= radiusInTiles; x++)
        {
            for (int y = -radiusInTiles; y <= radiusInTiles; y++)
            {
                Vector2Int checkPos = new Vector2Int(gridPosition.x + x, gridPosition.y + y);

                if (checkPos.x >= 0 && checkPos.x < cols && checkPos.y >= 0 && checkPos.y < rows)
                {
                    Vector3 tileCenter = gridArray[checkPos.y, checkPos.x].transform.position;
                    if ((tileCenter - gridArray[gridPosition.y, gridPosition.x].transform.position).sqrMagnitude <= radius * radius)
                    {
                        gridOwners[checkPos.y, checkPos.x] = player;
                    }
                }
            }
        }

        UpdateGridOwnershipDisplay();
    }

    void UpdateGridOwnershipDisplay()
    {
        int playerACount = 0;
        int playerBCount = 0;
        int defaultCount = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (gridOwners[row, col] == Player.PlayerA)
                {
                    playerACount++;
                }
                else if (gridOwners[row, col] == Player.PlayerB)
                {
                    playerBCount++;
                }
                else
                {
                    defaultCount++;
                }
            }
        }

        int totalGrids = rows * cols;
        float playerAPercent = (float)playerACount / totalGrids * 100;
        float playerBPercent = (float)playerBCount / totalGrids * 100;
        float defaultPercent = (float)defaultCount / totalGrids * 100;

        playerAText.text = $"Player A: {playerAPercent:F2}%";
        playerBText.text = $"Player B: {playerBPercent:F2}%";
    }

    public Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        float gridWidth = cols * tileSize;
        float gridHeight = rows * tileSize;
        Vector3 startPos = new Vector3(-gridWidth / 2 + tileSize / 2, -gridHeight / 2 + tileSize / 2, 0);

        int x = Mathf.FloorToInt((worldPosition.x - startPos.x) / tileSize);
        int y = Mathf.FloorToInt((worldPosition.y - startPos.y) / tileSize);

        return new Vector2Int(x, y);
    }
}
