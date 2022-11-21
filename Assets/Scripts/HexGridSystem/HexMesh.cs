using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    private Mesh hexMesh;
    private MeshCollider meshCollider;
    private List<Vector3> vertices;
    private List<int> triangles;
    private List<Color> colors;
    private MeshRenderer meshRenderer;

    public Color defaultColor = Color.white;
    public Color hoverColor = Color.yellow;

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        colors = new List<Color>();
    }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Renders mesh with the generated vertices and triangles
    public void Triangulate(HexCell[] cells)
    {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        colors.Clear();

        for (int i = 0; i < cells.Length; i++)
            Triangulate(cells[i]);

        hexMesh.vertices = vertices.ToArray();
        hexMesh.colors = colors.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.RecalculateNormals();
        meshCollider.sharedMesh = hexMesh;
    }

    // Adds data for one of six triangles that represent a hexagon
    private void Triangulate(HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(center, center + HexData.corners[i], center + HexData.corners[i + 1]);
            AddTriangleColor(cell.color);
        }
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertixIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertixIndex);
        triangles.Add(vertixIndex + 1);
        triangles.Add(vertixIndex + 2);
    }

    private void AddTriangleColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }

    private void OnMouseOver()
    {
        meshRenderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        meshRenderer.material.color = defaultColor;
    }
}
