using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreen : ScreenBase
{
    [SerializeField] private GameObject _levelButtonPrefab;
    [SerializeField] private GameObject _levelPanel;

    [SerializeField] private Button _backButton;


    protected override void Awake()
    {
        base.Awake();
        _backButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("Click");
            Navigation.Stack.GoBack();
        });
    }

    public override void OnEnter(object param = null)
    {
        base.OnEnter(param);
        List<LevelDataSO> listLevel = param as List<LevelDataSO>;
        LoadData(listLevel);

    }

    private void LoadData(List<LevelDataSO> listLevel)
    {
        _levelPanel.transform.ClearChildren();

        foreach (var levelData in listLevel)
        {
            GameObject levelButton = Instantiate(_levelButtonPrefab, _levelPanel.transform);
            LevelButtonUI button = levelButton.GetComponent<LevelButtonUI>();
            button.SetData(levelData);
        }
    }

}