using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Vẽ Inspector mặc định
        DrawDefaultInspector();

        // Lấy reference tới component
        GridManager gridManager = (GridManager)target;

        // Tạo nút
        if (GUILayout.Button("Generate Grid"))
        {
            gridManager.GenerateGrid();
        }
        if (GUILayout.Button("Clear Grid"))
        {
            gridManager.Clear();
        }


        // if (GUILayout.Button("Export Level"))
        // {
        //     gridManager.ExportLevel();


        // }

    }
}