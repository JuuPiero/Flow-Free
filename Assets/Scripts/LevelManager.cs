using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<LevelDataSO> easyLevels;
    public List<LevelDataSO> mediumLevels;
    public List<LevelDataSO> hardLevels;

    public LevelDataSO currentLevel;

    private List<LevelDataSO> GetCurrentLevelList()
    {
        if (easyLevels.Contains(currentLevel)) return easyLevels;
        if (mediumLevels.Contains(currentLevel)) return mediumLevels;
        if (hardLevels.Contains(currentLevel)) return hardLevels;

        return null;
    }

    public void NextLevel()
    {
        List<LevelDataSO> currentList = GetCurrentLevelList();
        if (currentList == null) return;

        int currentIndex = currentList.IndexOf(currentLevel);
        int nextIndex = currentIndex + 1;

        if (nextIndex < currentList.Count)
        {
            currentLevel = currentList[nextIndex];
        }
        else
        {
            Debug.Log("Đã hết level trong danh sách hiện tại.");
            // Có thể load lại từ đầu, chuyển difficulty, hoặc kết thúc game
        }
    }


}