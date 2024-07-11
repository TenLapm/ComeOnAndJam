using UnityEngine;

public class DrawMeshB : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player Transform
    [SerializeField] private DrawingTracker drawingTracker; // Reference to the DrawingTracker script (if tracking is needed)

    private Mesh sharedMesh; // Reference to the shared Mesh component
    private Vector3 lastPlayerPosition;

    private void Start()
    {
        // Get the MeshFilter component from the current GameObject
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            sharedMesh = meshFilter.sharedMesh;
        }
        else
        {
            Debug.LogError("MeshFilter component not found on the GameObject. Attach this script to a GameObject with a MeshFilter component.");
        }

        lastPlayerPosition = player.position;
    }

    private void Update()
    {
        float brushRadius = player.GetComponent<SpriteRenderer>().bounds.extents.x; // Use the player's size for the brush radius

        // Continue painting based on player input
        float minDistance = .1f;
        Vector3 playerPosition = player.position;
        if (Vector3.Distance(playerPosition, lastPlayerPosition) > minDistance)
        {
            UpdateMesh(playerPosition, brushRadius);
            lastPlayerPosition = playerPosition;
        }
    }

    private void UpdateMesh(Vector3 playerPosition, float brushRadius)
    {
        if (sharedMesh == null)
        {
            Debug.LogError("SharedMesh is not assigned. Make sure to assign it in the Inspector or find it programmatically.");
            return;
        }

        // Assuming you want to draw a simple circle for the brush
        Vector3[] vertices = sharedMesh.vertices;
        Vector2[] uv = sharedMesh.uv;
        int[] triangles = sharedMesh.triangles;

        int vertexIndex = vertices.Length;

        // Create vertices for a simple circle brush
        Vector3 playerForwardVector = (playerPosition - lastPlayerPosition).normalized;
        Vector3 normal2D = new Vector3(0, 0, -1f);
        Vector3 newVertexUp = playerPosition + Vector3.Cross(playerForwardVector, normal2D) * brushRadius;
        Vector3 newVertexDown = playerPosition + Vector3.Cross(playerForwardVector, normal2D * -1f) * brushRadius;

        // Update vertices
        vertices = new Vector3[] { lastPlayerPosition, newVertexUp, newVertexDown };

        // Update UVs
        uv = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

        // Update triangles
        triangles = new int[] {
            vertexIndex, vertexIndex + 1, vertexIndex + 2
        };

        // Assign updated mesh data
        sharedMesh.vertices = vertices;
        sharedMesh.uv = uv;
        sharedMesh.triangles = triangles;
        sharedMesh.RecalculateNormals(); // Recalculate normals if needed
        sharedMesh.RecalculateBounds(); // Recalculate bounds to update mesh bounds for rendering

        // Update drawn pixels count if using a tracker
        if (drawingTracker != null)
        {
            int drawnPixelCount = CalculateDrawnPixels(newVertexUp, newVertexDown);
            drawingTracker.AddDrawnPixels(drawnPixelCount);
        }
    }

    private int CalculateDrawnPixels(Vector3 vertexUp, Vector3 vertexDown)
    {
        // Approximate the number of pixels drawn based on the area of the triangle formed by the vertices
        float area = Mathf.Abs(Vector3.Cross(vertexUp - lastPlayerPosition, vertexDown - lastPlayerPosition).magnitude) / 2f;
        int pixelCount = Mathf.RoundToInt(area * 100); // Adjust this value as needed
        Debug.Log("Drawn Pixel Count: " + pixelCount); // Debug log to check pixel count
        return pixelCount;
    }
}
