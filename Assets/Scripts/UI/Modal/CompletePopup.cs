using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompletePopup : ScreenBase
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TMP_Text _turnsText;

    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _nextButton;


    public override void OnEnter(object param = null)
    {
        base.OnEnter(param);
        _turnsText.text = $"Turns {_gameManager.stepCount}/{_gameManager.maxStep}";
    }


    protected override void Awake()
    {
        base.Awake();

        _homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance?.PlaySFX("Click");
            SceneManager.LoadScene("MainMenu");
        });

        _resetButton.onClick.AddListener(() =>
        {
            AudioManager.Instance?.PlaySFX("Click");
            _gameManager?.ResetGame();
            Navigation.Modal.CloseModal();
        });

        _nextButton.onClick.AddListener(() =>
        {
            AudioManager.Instance?.PlaySFX("Click");
            _gameManager.NextLevel();
            Navigation.Modal.CloseModal();
        });
    }
   
}