using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public GridManager gridManager;
    public FlowManager flowManager;
    public InputHandler inputHandler;

    public int maxStep;
    public int stepCount;

    public event Action OnLevelChanged;


    private void Awake()
    {
        LoadLevel(LevelManager.Instance.currentLevel);
        maxStep = flowManager.Paths.Count;
    }

    public void LoadLevel(LevelDataSO levelData)
    {
        gridManager.LoadLevel(levelData);
        flowManager.SetColorPairs(levelData.colorPairs);
        OnLevelChanged?.Invoke();
    }

    public void ResetGame()
    {
        stepCount = 0;
        gridManager.LoadLevel(LevelManager.Instance.currentLevel);
        flowManager.SetColorPairs(LevelManager.Instance.currentLevel.colorPairs);
        OnLevelChanged?.Invoke();
    }


    public void ExportLevel()
    {
        // LevelDataSO levelData = ScriptableObject.CreateInstance<LevelDataSO>();
        // levelData.rows = gridManager.Rows;
        // levelData.columns = gridManager.Columns;
        // levelData.cellSize = gridManager.CellSize;

        // levelData.colorPairs = flowManager?.GetColorPairs();

        // string folder = "Assets/Levels";
        // if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);


        // string path = AssetDatabase.GenerateUniqueAssetPath($"{folder}/NewLevel.asset");

        // // Tạo asset
        // AssetDatabase.CreateAsset(levelData, path);
        // AssetDatabase.SaveAssets();
        // AssetDatabase.Refresh();

        // Debug.Log($"Level saved to: {path}");
        // EditorUtility.FocusProjectWindow();
        // Selection.activeObject = levelData;
    }

    public void NextLevel()
    {
        LevelManager.Instance.NextLevel();
        ResetGame();
    }
    
}