using Context;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameUi : Ui
    {
        [Header("Hud")] [SerializeField] private Text scoreText;

        [Header("GameOver")] [SerializeField] private GameOverPanel gameOverPanel;

        [Header("Pause")] [SerializeField] private GameObject pausePanel;

        [Header("Reward")] [SerializeField] private GameObject congratsPanel;

        public void UpdateHudScore() => scoreText.text = ContextProvider.Context.Score.CurrentPoints().ToString();


        #region PanelsActivateAndDeactivate

        public void GameOverPanelIn()
        {
            TurnOnUi(gameOverPanel.gameObject);
            gameOverPanel.UpdateTotalScore();
        } 
        public void GameOverPanelOut()
        {
            TurnOffUi(gameOverPanel.gameObject);
        }

        public void PauseIn()
        {
            ContextProvider.Context.GameManager.PauseGameToggle(true);
            TurnOnUi(pausePanel);
        }

        public void PauseOut()
        {
            ContextProvider.Context.GameManager.PauseGameToggle(false);
            TurnOffUi(pausePanel);
        }

        // When the player form the correct word this Method will be called.
        public void CongratsIn()
        {
            ContextProvider.Context.GameManager.PauseGameToggle(true);
            TurnOnUi(congratsPanel);
        }

        public void CongratsOut()
        {
            ContextProvider.Context.GameManager.PauseGameToggle(false);
            TurnOffUi(congratsPanel);
        }

        #endregion
    }
}