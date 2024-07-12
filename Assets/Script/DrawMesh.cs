using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMesh : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player Transform
    [SerializeField] private Transform leftBrush; // Reference to the left brush GameObject
    [SerializeField] private Transform rightBrush; // Reference to the right brush GameObject
    [SerializeField] private DrawingTracker drawingTracker; // Reference to the DrawingTracker script
    public bool isPlayer1; // True if this script belongs to Player 1, false for Player 2
    public Material drawingMaterial; // Reference to the material to use for drawing

    private Mesh mesh;
    private Vector3 lastPlayerPosition;

    private InventoryPowerUps IPU;
    private Vector3 sizeAdjust = new Vector3(3.0f, 3.0f, 3.0f);
    private Renderer meshRenderer;

    private void Awake()
    {
        // Initialize the MeshFilter component
        GetComponent<MeshFilter>().mesh = new Mesh();
        IPU = GetComponent<InventoryPowerUps>();

        // Ensure the Renderer component is attached
        meshRenderer = GetComponent<Renderer>();
        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        // Set the material
        meshRenderer.material = drawingMaterial;

        // Set the sorting order to ensure it renders on top
        meshRenderer.sortingLayerName = "Default";
        meshRenderer.sortingOrder = 10;
    }

    private void Start()
    {
        mesh = new Mesh();
        UpdateMesh();
        lastPlayerPosition = player.position;
    }

    private void FixedUpdate()
    {
        float brushRadius = player.GetComponent<SpriteRenderer>().bounds.extents.x;
        float minDistance = .1f;
        Vector3 playerPosition = player.position;

        if (Vector3.Distance(playerPosition, lastPlayerPosition) > minDistance)
        {
            // Use left and right brushes for drawing
            Vector3 leftBrushPosition = leftBrush.position;
            Vector3 rightBrushPosition = rightBrush.position;

            Vector3[] vertices = new Vector3[mesh.vertexCount + 4];
            Vector2[] uv = new Vector2[mesh.uv.Length + 4];
            int[] triangles = new int[mesh.triangles.Length + 6];

            mesh.vertices.CopyTo(vertices, 0);
            mesh.uv.CopyTo(uv, 0);
            mesh.triangles.CopyTo(triangles, 0);

            int vIndex = mesh.vertexCount;

            Vector3 playerForwardVector = (playerPosition - lastPlayerPosition).normalized;
            Vector3 normal2D = new Vector3(0, 0, -1f);
            Vector3 newVertexUp = playerPosition + Vector3.Cross(playerForwardVector, normal2D) * brushRadius;
            Vector3 newVertexDown = playerPosition + Vector3.Cross(playerForwardVector, -normal2D) * brushRadius;

            vertices[vIndex] = lastPlayerPosition;
            vertices[vIndex + 1] = lastPlayerPosition;
            vertices[vIndex + 2] = newVertexUp;
            vertices[vIndex + 3] = newVertexDown;

            uv[vIndex] = Vector2.zero;
            uv[vIndex + 1] = Vector2.zero;
            uv[vIndex + 2] = Vector2.zero;
            uv[vIndex + 3] = Vector2.zero;

            int tIndex = mesh.triangles.Length;

            triangles[tIndex] = vIndex;
            triangles[tIndex + 1] = vIndex + 2;
            triangles[tIndex + 2] = vIndex + 1;
            triangles[tIndex + 3] = vIndex + 1;
            triangles[tIndex + 4] = vIndex + 2;
            triangles[tIndex + 5] = vIndex + 3;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            // Update drawn pixels count
            int drawnPixelCount = CalculateDrawnPixels(vIndex, vIndex + 1, vIndex + 2, vIndex + 3);
            drawingTracker.AddDrawnPixels(drawnPixelCount);

            lastPlayerPosition = playerPosition;

            if (Input.GetKeyDown(KeyCode.L))
            {
                player.localScale = Vector3.Scale(player.localScale, sizeAdjust);
            }

            UpdateMesh();
        }
    }

    private void UpdateMesh()
    {
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private int CalculateDrawnPixels(int vIndex0, int vIndex1, int vIndex2, int vIndex3)
    {
        Vector3[] vertices = mesh.vertices;
        float area = Mathf.Abs(Vector3.Cross(vertices[vIndex1] - vertices[vIndex0], vertices[vIndex2] - vertices[vIndex0]).z) / 2f
                   + Mathf.Abs(Vector3.Cross(vertices[vIndex2] - vertices[vIndex1], vertices[vIndex3] - vertices[vIndex1]).z) / 2f;
        int pixelCount = Mathf.RoundToInt(area * 100); // Adjust this value as needed
        Debug.Log("Drawn Pixel Count: " + pixelCount);
        return pixelCount;
    }
}