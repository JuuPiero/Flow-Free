using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "LevelData")]
public class LevelDataSO : ScriptableObject
{
    public int levelIndex;
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public List<ColorPair> colorPairs;
}
