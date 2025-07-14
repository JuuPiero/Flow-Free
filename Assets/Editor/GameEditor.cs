using UnityEngine;
using UnityEditor;
using System;
using System.IO;
[CustomEditor(typeof(GameManager))]
public class GameEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        // Lấy reference tới component
        GameManager gameManager = (GameManager)target;

        // Tạo nút
        if (GUILayout.Button("Export Level"))
        {
            // gameManager.ExportLevel();

            LevelDataSO levelData = ScriptableObject.CreateInstance<LevelDataSO>();
            levelData.rows = gameManager.gridManager.Rows;
            levelData.columns = gameManager.gridManager.Columns;
            levelData.cellSize = gameManager.gridManager.CellSize;

            levelData.colorPairs = gameManager.flowManager?.GetColorPairs();

            string folder = "Assets/Levels";
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);


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
}