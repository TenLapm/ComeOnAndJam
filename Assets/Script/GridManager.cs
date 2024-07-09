using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 10;
    public int cols = 10;
    public float tileSize = 1.0f;
    public GameObject tilePrefab;

    private GameObject[,] gridArray;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        gridArray = new GameObject[rows, cols];

        float gridWidth = cols * tileSize;
        float gridHeight = rows * tileSize;
        Vector3 startPos = new Vector3(-gridWidth / 2 + tileSize / 2, -gridHeight / 2 + tileSize / 2, 0);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 pos = startPos + new Vector3(col * tileSize, row * tileSize, 0);
                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tile.transform.SetParent(this.transform);
                tile.name = $"Tile_{row}_{col}";
                gridArray[row, col] = tile;
            }
        }
    }

    public void ChangeTileColor(Vector2Int gridPosition, Color color, float radius)
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
                    if (Vector3.Distance(tileCenter, gridArray[gridPosition.y, gridPosition.x].transform.position) <= radius)
                    {
                        gridArray[checkPos.y, checkPos.x].GetComponent<Renderer>().material.color = color;
                    }
                }
            }
        }
    }

    public Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x + cols * tileSize / 2) / tileSize);
        int y = Mathf.FloorToInt((worldPosition.y + rows * tileSize / 2) / tileSize);
        return new Vector2Int(x, y);
    }
}
