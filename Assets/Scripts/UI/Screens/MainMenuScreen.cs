using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : ScreenBase
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    protected override void Awake()
    {
        base.Awake();

        _playButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("Click");
            Navigation.Stack.Navigate("DifficultyScreen");
        });

        _quitButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("Click");
            Application.Quit();
        });
    }

}