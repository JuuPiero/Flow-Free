using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _cellPrefab;

    [SerializeField] private int _rows;
    public int Rows => _rows;
    [SerializeField] private int _columns;
    public int Columns => _columns;

    [SerializeField] private Vector2 _margin = new(5f, 0f);

    [SerializeField] private Vector2 _spacing = new(0f, 0f);
    public Vector2 Spacing => _spacing;
    [SerializeField] private Vector2 _cellSize;
    public Vector2 CellSize => _cellSize;
    [field: SerializeField] public List<Cell> AllCells = new();

    void Awake()
    {
        //AllCells = transform.GetChildren<Cell>();
    }

    public void GenerateGrid()
    {
        Clear();
        Vector2 totalCellSize = _cellSize + _spacing;
        Vector2 gridOrigin = new Vector2(
            -(_columns - 1) * totalCellSize.x / 2f,
            -(_rows - 1) * totalCellSize.y / 2f
        );

        for (int y = 0; y < _rows; y++)
        {
            for (int x = 0; x < _columns; x++)
            {
                int yInverted = _rows - 1 - y;
                Vector2 pos = new Vector2(x * totalCellSize.x, yInverted * totalCellSize.y) + gridOrigin;
                GameObject cellGO = Instantiate(_cellPrefab, pos, Quaternion.identity, transform);
                cellGO.name = $"Cell ({x},{y})";
                cellGO.transform.localScale = _cellSize;
                Cell cell = cellGO.GetComponent<Cell>();
                cell.SetPosition(x, y);
                AllCells.Add(cell);
            }
        }
        // AllCells = transform.GetChildren<Cell>();
        AdjustCameraToFitGrid();
    }


    public void Clear()
    {
        transform.ClearChildren();
        AllCells.Clear();
    }

    public Cell GetCell(Vector2Int pos)
    {
        var cell = AllCells.Find(c => c.Position == pos);
        return cell;
    }
    public Cell GetCellAtPosition(Vector2 position)
    {
        return AllCells.Find(c => (Vector2)c.transform.position == position);
    }

    public void LoadLevel(LevelDataSO levelData)
    {
        _rows = levelData.rows;
        _columns = levelData.columns;
        _cellSize = levelData.cellSize;
        GenerateGrid();
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        AllCells?.ForEach(cell =>
        {
            Handles.Label(cell.transform.position, $"{cell.Position.x},{cell.Position.y}");
        });
    }
#endif
    void AdjustCameraToFitGrid()
    {
        float cellWidth = _cellSize.x;
        float cellHeight = _cellSize.y;
        float spacingX = _spacing.x;
        float spacingY = _spacing.y;

        float totalWidth = _columns * cellWidth + (_columns - 1) * spacingX + _margin.x * 2;
        float totalHeight = _rows * cellHeight + (_rows - 1) * spacingY + _margin.y * 2;

        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = totalWidth / totalHeight;

        Camera cam = Camera.main;

        if (screenRatio >= targetRatio)
        {
            cam.orthographicSize = totalHeight / 2f;
        }
        else
        {
            float adjustedHeight = totalWidth / screenRatio;
            cam.orthographicSize = adjustedHeight / 2f;
        }

        cam.transform.position = new Vector3(0, 0, -10);
    }

}