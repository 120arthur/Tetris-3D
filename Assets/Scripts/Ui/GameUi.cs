using Context;
using UnityEngine;
using UnityEngine.UI;


public class GameUi : Ui
{
    [Header("Hud")] [SerializeField] private Text scoreText;

    [Header("GameOver")] [SerializeField] private Text totalScore;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Pause")] [SerializeField] private GameObject pausePanel;

    [Header("Reward")] [SerializeField] private GameObject congratsPanel;

    public void UpdateHudScore()
    {
        scoreText.text = ContextProvider.Context.gameManager.Score.CurrentPoints().ToString();
    }

    // Updates game over panel points
    private void UpdateTotalScore()
    {
        totalScore.text = ContextProvider.Context.gameManager.Score.CurrentPoints().ToString();
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
        ContextProvider.Context.gameManager.PauseGameToggle(true);
    }

    public void CongratsOut()
    {
        TurnOffUi(congratsPanel);
        ContextProvider.Context.gameManager.PauseGameToggle(false);
    }

    #endregion
}