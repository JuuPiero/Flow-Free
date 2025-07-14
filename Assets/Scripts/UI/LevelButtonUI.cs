using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour
{
    [SerializeField] private LevelDataSO _levelData;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _levelText;


    private void Awake()
    {
        _button = GetComponent<Button>();
        _button?.onClick.AddListener(PlayGame);
    }


    public void SetData(LevelDataSO levelData)
    {
        _levelData = levelData;
        _levelText.text = _levelData.name;
    }


    public void PlayGame()
    {
        LevelManager.Instance.currentLevel = _levelData;
        SceneManager.LoadScene("Game");
    }

}