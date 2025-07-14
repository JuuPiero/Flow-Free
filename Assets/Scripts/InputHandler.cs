using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private FlowManager _flowManager;

    [SerializeField] private GameManager _gameManager;

    private bool _isDragging = false;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            Cell cell = GetCellAt(worldPos);
            if (cell != null)
            {
                _isDragging = true;
                _flowManager.BeginPath(cell);
                _gameManager.stepCount++;
            }
        }
        else if (Input.GetMouseButton(0) && _isDragging)
        {
            Vector2 worldPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            Cell cell = GetCellAt(worldPos);
            if (cell != null)
                _flowManager.ExtendPath(cell);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_isDragging)
            {
                _flowManager.EndPath();
                _isDragging = false;
            }
        }
    }
 

    private Cell GetCellAt(Vector2 worldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null)
            return hit.collider.GetComponent<Cell>();
        return null;
    }
}