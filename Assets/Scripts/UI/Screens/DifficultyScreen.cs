using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyScreen : ScreenBase
{
    [SerializeField] private Button _backButton;

    [SerializeField] private TMP_Text _easyLevelText;
    [SerializeField] private TMP_Text _mediumLevelText;
    [SerializeField] private TMP_Text _hardLevelText;


    [SerializeField] private Button _easyLevelButton;
    [SerializeField] private Button _mediumLevelButton;
    [SerializeField] private Button _hardLevelButton;


    protected override void Awake()
    {
        base.Awake();
        _backButton.onClick.AddListener(() =>
        {
            Navigation.Stack.GoBack();
        });

        _easyLevelButton.onClick.AddListener(() =>
        {
            Navigation.Stack.Navigate("LevelScreen", LevelManager.Instance.easyLevels);
        });

        _mediumLevelButton.onClick.AddListener(() =>
        {
            Navigation.Stack.Navigate("LevelScreen", LevelManager.Instance.mediumLevels);
        });

        _hardLevelButton?.onClick.AddListener(() =>
        {
            Navigation.Stack.Navigate("LevelScreen", LevelManager.Instance.hardLevels);
        });
    }

    private void LoadData()
    {
        
    }
}