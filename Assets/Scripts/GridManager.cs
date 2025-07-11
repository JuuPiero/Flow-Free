using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private FlowManager _flowManager;

    [SerializeField] private GameObject _cellPrefab;

    [SerializeField] private int _rows;
    [SerializeField] private int _columns;
    [SerializeField] private Vector2 _spacing = new(0f, 0f);
    [SerializeField] private Vector2 _cellSize;
    [field: SerializeField] public List<Cell> AllCells = new();

    void Awake()
    {
        AllCells = transform.GetChildren<Cell>();
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
                // _allCells.Add(cell);
            }
        }
        AllCells = transform.GetChildren<Cell>();
    }


    void Clear()
    {
        transform.ClearChild();
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
        _flowManager?.SetColorPairs(levelData.colorPairs);
    }

    public void ExportLevel()
    {
        LevelDataSO levelData = ScriptableObject.CreateInstance<LevelDataSO>();
        levelData.rows = _rows;
        levelData.columns = _columns;
        levelData.colorPairs = _flowManager?.GetColorPairs();
        levelData.cellSize = _cellSize;

        string folder = "Assets/Levels";
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        // Đặt tên file không trùng lặp
        string path = AssetDatabase.GenerateUniqueAssetPath($"{folder}/NewLevel.asset");

        // Tạo asset
        AssetDatabase.CreateAsset(levelData, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Level saved to: {path}");
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = levelData;
    }
}