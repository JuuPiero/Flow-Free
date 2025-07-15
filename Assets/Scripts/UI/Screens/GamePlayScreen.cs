using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayScreen : ScreenBase
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _replayButton;

    [SerializeField] private Button _musicButton;

    [SerializeField] private Image _musicImage;
    [SerializeField] private Sprite _musicEnableSprite;
    [SerializeField] private Sprite _musicDisableSprite;
    
    [SerializeField] private TMP_Text _levelText;


    void Start()
    {
        _gameManager.OnLevelChanged += UpdateLevelText;
        UpdateLevelText();
    }
    void OnDisable()
    {
        _gameManager.OnLevelChanged -= UpdateLevelText;
    }

    protected override void Awake()
    {
        base.Awake();

        _homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("Click");
            SceneManager.LoadScene("MainMenu");
        });

        _replayButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("Click");
            _gameManager?.ResetGame();
        });

        _musicButton.onClick.AddListener(ToggleMusic);
    }


    private void UpdateLevelText()
    {
        _levelText.text = "Level " + LevelManager.Instance.currentLevel.name;
    }



    private void ToggleMusic()
    {
        AudioManager.Instance.PlaySFX("Click");
        if (AudioManager.Instance.bgmSource.isPlaying)
        {
            AudioManager.Instance.StopBGM();
            _musicImage.sprite = _musicDisableSprite;
        }
        else
        {
            AudioManager.Instance.PlayBGM("Main");
            _musicImage.sprite = _musicEnableSprite;
        }

    }

}