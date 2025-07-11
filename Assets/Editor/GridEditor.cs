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


        if (GUILayout.Button("Export Level"))
        {
            gridManager.ExportLevel();

            // LevelDataSO levelData = ScriptableObject.CreateInstance<LevelDataSO>();
            // levelData.rows = _rows;
            // levelData.columns = _columns;

            // string folder = "Assets/Levels";
            // if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            // // Đặt tên file không trùng lặp
            // string path = AssetDatabase.GenerateUniqueAssetPath($"{folder}/NewLevel.asset");

            // // Tạo asset
            // AssetDatabase.CreateAsset(levelData, path);
            // AssetDatabase.SaveAssets();
            // AssetDatabase.Refresh();

            // Debug.Log($"Level saved to: {path}");
            // EditorUtility.FocusProjectWindow();
            // Selection.activeObject = levelData;

        }


    }
}