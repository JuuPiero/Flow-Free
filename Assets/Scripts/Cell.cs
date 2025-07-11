using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [field: SerializeField] public Vector2Int Position { get; private set; }
    [SerializeField] public bool IsDot { get; set; }
    [field: SerializeField] public Color? CurrentColor { get; private set; }
    [SerializeField] private SpriteRenderer _dotSR;
    [SerializeField] private SpriteRenderer _bgRenderer;

    private void Awake() {
        _dotSR.gameObject.SetActive(false);
    } 
    public void SetPosition(int x, int y)
    {
        Position = new Vector2Int(x, y);
    }

    public void SetColor(Color color)
    {
        _dotSR.gameObject.SetActive(true);
        CurrentColor = color;
        _dotSR.color = color;
    }
    public void SetBackgroundColor(Color color) {
        color.a = 0.5f;
        _bgRenderer.color = color;
    }

    public void ClearColor()
    {
        Color clearColor = Color.white;
        clearColor.a = 0f;
        _bgRenderer.color = clearColor;
    }
}