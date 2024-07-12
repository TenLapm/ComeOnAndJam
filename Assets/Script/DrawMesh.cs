using System.Collections.Generic;
using UnityEngine;

public class DrawMesh : MonoBehaviour
{
    [SerializeField] private Transform player1; 
    [SerializeField] private Transform player2; 
    [SerializeField] private float brushRadius = 1f; 

    [Header("Paint Colors")]
    [SerializeField] private Color player1Color = Color.red; 
    [SerializeField] private Color player2Color = Color.blue; 

    [Header("Materials")]
    [SerializeField] private Material player1Material; 
    [SerializeField] private Material player2Material; 

    private Mesh mesh1;
    private Mesh mesh2;
    private Vector3 lastPlayer1Position;
    private Vector3 lastPlayer2Position;
    private List<Vector3> vertices1 = new List<Vector3>();
    private List<Vector3> vertices2 = new List<Vector3>();
    private List<int> triangles1 = new List<int>();
    private List<int> triangles2 = new List<int>();
    private List<Color> colors1 = new List<Color>();
    private List<Color> colors2 = new List<Color>();

    private void Awake()
    {
        mesh1 = new Mesh();
        mesh2 = new Mesh();

        GameObject brush1 = new GameObject("Brush1");
        brush1.AddComponent<MeshFilter>().mesh = mesh1;
        brush1.AddComponent<MeshRenderer>().material = player1Material;

        GameObject brush2 = new GameObject("Brush2");
        brush2.AddComponent<MeshFilter>().mesh = mesh2;
        brush2.AddComponent<MeshRenderer>().material = player2Material;

        brush1.transform.SetParent(transform);
        brush2.transform.SetParent(transform);
    }

    private void Update()
    {
        PaintPlayer(player1, ref lastPlayer1Position, mesh1, vertices1, triangles1, colors1, player1Color); 
        PaintPlayer(player2, ref lastPlayer2Position, mesh2, vertices2, triangles2, colors2, player2Color); 
    }

    private void PaintPlayer(Transform player, ref Vector3 lastPlayerPosition, Mesh mesh, List<Vector3> vertices, List<int> triangles, List<Color> colors, Color brushColor)
    {
        float minDistance = .1f;
        Vector3 playerPosition = player.position;

        if (Vector3.Distance(playerPosition, lastPlayerPosition) > minDistance)
        {
            int vIndex = vertices.Count;

            Vector3 playerForwardVector = (playerPosition - lastPlayerPosition).normalized;
            Vector3 normal2D = new Vector3(0, 0, -1f);
            Vector3 newVertexUp = playerPosition + Vector3.Cross(playerForwardVector, normal2D) * brushRadius;
            Vector3 newVertexDown = playerPosition + Vector3.Cross(playerForwardVector, normal2D * -1f) * brushRadius;

            vertices.Add(newVertexUp);
            vertices.Add(newVertexDown);
            colors.Add(brushColor);
            colors.Add(brushColor);

            if (vIndex > 0)
            {
                triangles.Add(vIndex - 2);
                triangles.Add(vIndex);
                triangles.Add(vIndex - 1);

                triangles.Add(vIndex - 1);
                triangles.Add(vIndex);
                triangles.Add(vIndex + 1);
            }

            
            UpdateMesh(mesh, vertices, triangles, colors);
            lastPlayerPosition = playerPosition;
        }
    }

    private void UpdateMesh(Mesh mesh, List<Vector3> vertices, List<int> triangles, List<Color> colors)
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors.ToArray();
        mesh.RecalculateNormals(); 
    }
}
