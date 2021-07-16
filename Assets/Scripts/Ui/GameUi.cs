using Context;
using UnityEngine;
using UnityEngine.UI;


public class GameUi : Ui
{
    [Header("Hud")] [SerializeField] private Text _scoreText;

    [Header("GameOver")] [SerializeField] private GameOverPanel _gameOverPanel;

    [Header("Pause")] [SerializeField] private GameObject _pausePanel;

    [Header("Reward")] [SerializeField] private GameObject _congratsPanel;

    public void UpdateHudScore()
    {
        _scoreText.text = ContextProvider.Context.Score.CurrentPoints().ToString();
    }

    #region PanelsActivateAndDeactivate

    public void GameOverPanelIn()
    {
        _gameOverPanel.Show();
        _gameOverPanel.UpdateTotalScore();
    }

    public void PauseIn()
    {
        TurnOnUi(_pausePanel);
    }

    public void PauseOut()
    {
        TurnOffUi(_pausePanel);
    }

    // When the player form the correct word this Method will be called.
    public void CongratsIn()
    {
        TurnOnUi(_congratsPanel);
        ContextProvider.Context.GameManager.PauseGameToggle(true);
    }

    public void CongratsOut()
    {
        TurnOffUi(_congratsPanel);
        ContextProvider.Context.GameManager.PauseGameToggle(false);
    }

    #endregion
}