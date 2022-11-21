using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HexGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;
    public Color defaultColor = Color.white;
    public Color touchedColor = Color.cyan;

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
        cell.color = defaultColor;
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

    private void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            HandleInput();
    }

    private void HandleInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            TouchCell(hit.point);
    }

    public void TouchCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);

        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.color = touchedColor;
        hexMesh.Triangulate(cells);

        Debug.Log("Touched At: " + coordinates.ToString());
    }
}
