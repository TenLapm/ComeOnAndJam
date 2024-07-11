using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMesh : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player Transform
    [SerializeField] private DrawingTracker drawingTracker; // Reference to the DrawingTracker script
    public bool isPlayer1; // True if this script belongs to Player 1, false for Player 2

    private Mesh mesh;
    private Vector3 lastPlayerPosition;

    private void Awake()
    {
        // Initialize the MeshFilter component
        GetComponent<MeshFilter>().mesh = new Mesh();
    }

    private void Start()
    {
        // Start painting immediately
        mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        Vector3 playerPosition = player.position;
        vertices[0] = playerPosition;
        vertices[1] = playerPosition;
        vertices[2] = playerPosition;
        vertices[3] = playerPosition;

        uv[0] = Vector2.zero;
        uv[1] = Vector2.zero;
        uv[2] = Vector2.zero;
        uv[3] = Vector2.zero;

        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;

        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.MarkDynamic();

        GetComponent<MeshFilter>().mesh = mesh;

        lastPlayerPosition = playerPosition;
    }

    private void Update()
    {
        float brushRadius = player.GetComponent<SpriteRenderer>().bounds.extents.x; // Use the player's size for the brush radius

        // Continue painting based on player input
        float minDistance = .1f;
        Vector3 playerPosition = player.position;
        if (Vector3.Distance(playerPosition, lastPlayerPosition) > minDistance)
        {
            Vector3[] vertices = new Vector3[mesh.vertices.Length + 2];
            Vector2[] uv = new Vector2[mesh.uv.Length + 2];
            int[] triangles = new int[mesh.triangles.Length + 6];

            mesh.vertices.CopyTo(vertices, 0);
            mesh.uv.CopyTo(uv, 0);
            mesh.triangles.CopyTo(triangles, 0);

            int vIndex = vertices.Length - 4;
            int vIndex0 = vIndex + 0;
            int vIndex1 = vIndex + 1;
            int vIndex2 = vIndex + 2;
            int vIndex3 = vIndex + 3;

            Vector3 playerForwardVector = (playerPosition - lastPlayerPosition).normalized;
            Vector3 normal2D = new Vector3(0, 0, -1f);
            Vector3 newVertexUp = playerPosition + Vector3.Cross(playerForwardVector, normal2D) * brushRadius;
            Vector3 newVertexDown = playerPosition + Vector3.Cross(playerForwardVector, normal2D * -1f) * brushRadius;

            vertices[vIndex2] = newVertexUp;
            vertices[vIndex3] = newVertexDown;

            uv[vIndex2] = Vector2.zero;
            uv[vIndex3] = Vector2.zero;

            int tIndex = triangles.Length - 6;

            triangles[tIndex + 0] = vIndex0;
            triangles[tIndex + 1] = vIndex2;
            triangles[tIndex + 2] = vIndex1;

            triangles[tIndex + 3] = vIndex1;
            triangles[tIndex + 4] = vIndex2;
            triangles[tIndex + 5] = vIndex3;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            // Update drawn pixels count
            int drawnPixelCount = CalculateDrawnPixels(vIndex0, vIndex1, vIndex2, vIndex3);
            drawingTracker.AddDrawnPixels(drawnPixelCount);

            lastPlayerPosition = playerPosition;
        }
    }

    private int CalculateDrawnPixels(int vIndex0, int vIndex1, int vIndex2, int vIndex3)
    {
        // Approximate the number of pixels drawn based on the area of the triangle
        Vector3[] vertices = mesh.vertices;
        float area = Mathf.Abs(Vector3.Cross(vertices[vIndex1] - vertices[vIndex0], vertices[vIndex2] - vertices[vIndex0]).z) / 2f
                   + Mathf.Abs(Vector3.Cross(vertices[vIndex2] - vertices[vIndex1], vertices[vIndex3] - vertices[vIndex1]).z) / 2f;
        int pixelCount = Mathf.RoundToInt(area * 100); // Adjust this value as needed
        Debug.Log("Drawn Pixel Count: " + pixelCount); // Debug log to check pixel count
        return pixelCount;
    }
}
