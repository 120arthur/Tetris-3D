using UnityEngine;
using UnityEngine.UI;


public class GameUi : Ui
{
    [Header("Hud")] [SerializeField] private Text scoreText;

    [Header("GameOver")] [SerializeField] private Text totalScore;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Pause")] [SerializeField] private GameObject pausePanel;

    [Header("Reward")] [SerializeField] private GameObject congratsPanel;

    private GameManager _gameManagerInstance;

    private void Start()
    {
        _gameManagerInstance = GameManager.GameManagerInstance;
    }

    public void UpdateHudScore()
    {
        scoreText.text = _gameManagerInstance.Score.CurrentPoints().ToString();
    }

    // Updates game over panel points
    private void UpdateTotalScore()
    {
        totalScore.text = _gameManagerInstance.Score.CurrentPoints().ToString();
    }

    #region PanelsActivateAndDeactivate

    public void GameOverPanelIn()
    {
        gameOverPanel.SetActive(true);
        UpdateTotalScore();
    }

    public void PauseIn()
    {
        TurnOnUi(pausePanel);
    }

    public void PauseOut()
    {
        TurnOffUi(pausePanel);
    }

    // When the player form the correct word this Method will be called.
    public void CongratsIn()
    {
        TurnOnUi(congratsPanel);
        _gameManagerInstance.PauseGameToggle(true);
    }

    public void CongratsOut()
    {
        TurnOffUi(congratsPanel);
        _gameManagerInstance.PauseGameToggle(false);
    }

    #endregion
}