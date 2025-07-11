using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Path
{
    public Color? color;
    // public List<Vector2> points = new();
    public List<Cell> cells = new();
    public LineRenderer lineRenderer;


    public int CellCount => cells.Count;
    public Cell LastCell => cells[^1];
    public Cell FirstCell => cells[0];


    public bool Contains(Cell cell)
    {
        return cells.Contains(cell);
    }

    public void Add(Cell cell)
    {
        cells.Add(cell);
    }

    public void Clear()
    {
        lineRenderer.positionCount = 0;
        cells.Clear();
    }

    // draw the path on the line renderer
    public void DrawPath()
    {
        lineRenderer.positionCount = cells.Count;
        lineRenderer.SetPositions(cells.ConvertAll(c => (Vector3)c.transform.position).ToArray());
    }
}