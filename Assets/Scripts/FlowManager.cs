using System.Collections.Generic;
using UnityEngine;


public class FlowManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private GameObject _linePrefab;

    [SerializeField] private List<ColorPair> _colorPairs;
    [SerializeField] private List<Path> _paths = new();
    public List<Path> Paths => _paths;
    [SerializeField] private Path _currentPath = new();
    bool _isDrawing;

    // public event Action On 

    public void SetColorPairs(List<ColorPair> colorPairs)
    {
        _colorPairs = colorPairs;
        GenerateFlow();
        GeneratePathTemplate();
    }

    public List<ColorPair> GetColorPairs()
    {
        return _colorPairs;
    }

    private void Start()
    {
        // GenerateFlow();
        // GeneratePathTemplate();
    }

    public void GenerateFlow()
    {
        Clear();
        foreach (var pair in _colorPairs)
        {
            var gridManager = _gameManager.gridManager;
            Cell startCell = gridManager.GetCell(pair.start);
            Cell endCell = gridManager.GetCell(pair.end);
            startCell.IsDot = true;
            endCell.IsDot = true;
            startCell.SetColor(pair.color);
            endCell.SetColor(pair.color);
        }
    }

    private void GeneratePathTemplate()
    {
        foreach (var pair in _colorPairs)
        { 
            GameObject pathGO = Instantiate(_linePrefab, transform.position, Quaternion.identity, transform);
            LineRenderer lr = pathGO.GetComponent<LineRenderer>();
            lr.material.color = pair.color;
            _paths.Add(new Path
            {
                color = pair.color,
                lineRenderer = lr
            });
        }
    }

    public void Clear()
    {
        transform.ClearChildren();
        _paths.Clear();
    }
    public void BeginPath(Cell cell)
    {
        foreach (var path in _paths)
        {
            // Nếu bấm vào điểm cuối của path nào đó CHƯA HOÀN THÀNH → tiếp tục kéo
            if (path.CellCount > 0 && path.LastCell == cell && !IsPathCompleted(path))
            {
                _currentPath = path;
                _isDrawing = true;
                return;
            }

            // Nếu bấm vào giữa path → cắt phần sau và tiếp tục
            if (path.CellCount > 0 && path.Contains(cell) && !IsPathCompleted(path))
            {
                int index = path.cells.IndexOf(cell);
                path.cells.RemoveRange(index + 1, path.CellCount - index - 1);

                _currentPath = path;
                _isDrawing = true;
                UpdatePath(_currentPath);
                return;
            }
        }

        // Nếu cell là Dot
        if (cell.IsDot && cell.CurrentColor.HasValue)
        {
            Color color = cell.CurrentColor.Value;
            Path path = _paths.Find(p => p.color == color);

            if (path != null && IsPathCompleted(path))
            {
                if (cell != path.FirstCell && cell != path.LastCell)
                    return;

                path.Clear();
            }

            StartNewPath(cell, path);
        }
    }

    public void ExtendPath(Cell cell)
    {
        if (!_isDrawing || _currentPath == null) return;

        // Không cho đè lên path khác màu
        foreach (var path in _paths)
        {
            if (path.color != _currentPath.color && path.cells.Contains(cell))
                return;
        }
        // Nếu đi vào Dot khác màu
        if (cell.IsDot && cell.CurrentColor != _currentPath.color)
            return;

        // Nếu đi vào Dot cùng màu (đích đến)
        if (cell.IsDot && cell.CurrentColor == _currentPath.color)
        {
            if (!_currentPath.Contains(cell))
            {
                _currentPath.Add(cell);
                UpdatePath(_currentPath);
                EndPath(); // Kết thúc path luôn
            }
            return; // Không cho vẽ tiếp
        }

        // Nếu không liền kề
        Vector2Int a = _currentPath.LastCell.Position;
        Vector2Int b = cell.Position;
        int dx = Mathf.Abs(a.x - b.x);
        int dy = Mathf.Abs(a.y - b.y);
        if (!((dx == 1 && dy == 0) || (dx == 0 && dy == 1)))
            return; // Không liền kề theo trục ngang/dọc


        //  Nếu đi lùi lại
        if (_currentPath.Contains(cell))
        {
            int index = _currentPath.cells.IndexOf(cell);
            _currentPath.cells.RemoveRange(index + 1, _currentPath.CellCount - index - 1);
            UpdatePath(_currentPath);
            return;
        }

        // Hợp lệ => thêm vào path
        _currentPath.Add(cell);
        UpdatePath(_currentPath);
    }
    public void EndPath()
    {
        if (!_isDrawing || _currentPath == null || _currentPath.CellCount < 2) return;
        UpdatePath(_currentPath);
        _currentPath = null;
        _isDrawing = false;
        if (IsWin())
        {
            // Debug.Log("You Win 123!");
            Navigation.Modal?.ShowModal("CompletePopup");
        }
    }

    void UpdatePath(Path changedPath)
    {
        changedPath.DrawPath();

        foreach (var cell in _gameManager.gridManager.AllCells)
        {
            cell.ClearColor(); // Mặc định xóa nền trước
        }

        foreach (var path in _paths)
        {
            foreach (var cell in path.cells)
            {
                cell.SetBackgroundColor(path.color.Value);
            }
        }
    }

    void StartNewPath(Cell cell, Path basePath)
    {
        _currentPath = basePath;
        _currentPath.Clear();
        _currentPath.Add(cell);
        _isDrawing = true;
    }

    private bool IsPathCompleted(Path path)
    {
        if (path.CellCount < 2) return false;

        Cell startCell = path.FirstCell;
        Cell endCell = path.LastCell;

        return startCell != null && startCell.IsDot &&
               endCell != null && endCell.IsDot;
    }

    private bool IsWin()
    {
        foreach (var path in _paths)
        {
            if (!IsPathCompleted(path))
                return false;
        }

        // Check: Mọi ô đều được phủ bởi path (trừ Dot)
        foreach (var cell in _gameManager.gridManager.AllCells)
        {
            if (cell.IsDot) continue;

            bool isCovered = false;
            foreach (var path in _paths)
            {
                if (path.Contains(cell))
                {
                    isCovered = true;
                    break;
                }
            }

            if (!isCovered)
                return false;
        }
        return true;
    }

}