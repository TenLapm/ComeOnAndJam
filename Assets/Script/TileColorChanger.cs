using UnityEngine;
using UnityEngine.Tilemaps;

public class TileColorChanger : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile player1Tile; // Assign a tile with player 1's color
    public Tile player2Tile; // Assign a tile with player 2's color

    private void Start()
    {
        if (tilemap == null)
        {
            tilemap = GetComponent<Tilemap>();
        }
    }

    public void ChangeTileColor(Vector3Int position, int playerNumber)
    {
        Tile tileToPlace = playerNumber == 1 ? player1Tile : player2Tile;
        tilemap.SetTile(position, tileToPlace);
    }
}
