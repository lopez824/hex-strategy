using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HexGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    [SerializeField]
    private TextMeshProUGUI cellDebugLabel;

    [SerializeField]
    private HexCell cellPrefab;

    private Canvas gridCanvas;
    private HexMesh hexMesh;
    private HexCell[] cells;

    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>(); 
        hexMesh = GetComponentInChildren<HexMesh>();
        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
            for (int x = 0; x < width; x++)
                CreateCell(x, z, i++);
    }

    private void Start()
    {
        hexMesh.Triangulate(cells);
    }

    // Creates a cell within the grid givin x and z positions
    private void CreateCell(int x, int z, int index)
    {
        Vector3 position;
        position.x = (x + z * 0.5f -z/2) * (HexData.innerRadius * 2f);  // adjusts offset and distance between hexagonal cells
        position.y = 0f;
        position.z = z * (HexData.outerRadius * 1.5f);

        HexCell cell = cells[index] = Instantiate(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffset(x, z);
        AddDebugLabel(cell, position, x, z);
    }

    // Creates a debug label for each cell if called
    private void AddDebugLabel(HexCell cell, Vector3 position, int x, int z)
    {
        TextMeshProUGUI label = Instantiate(cellDebugLabel);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringNewLine();
    }
}
