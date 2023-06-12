using Score;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    public class GameUi : Ui
    {
        [Inject]
        private ControllerManager m_ControllerManager;
        [Inject]
        private ScoreManager m_ScoreManager;

        [Header("Hud")] [SerializeField] private Text scoreText;

        [Header("GameOver")] [SerializeField] private GameOverPanel gameOverPanel;

        [Header("Pause")] [SerializeField] private GameObject pausePanel;

        [Header("Reward")] [SerializeField] private GameObject congratsPanel;

        public void UpdateHudScore() => scoreText.text = m_ScoreManager.CurrentPoints().ToString();


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
            m_ControllerManager.PauseGameToggle(true);
            TurnOnUi(pausePanel);
        }

        public void PauseOut()
        {
            m_ControllerManager.PauseGameToggle(false);
            TurnOffUi(pausePanel);
        }

        // When the player form the correct word this Method will be called.
        public void CongratsIn()
        {
            m_ControllerManager.PauseGameToggle(true);
            TurnOnUi(congratsPanel);
        }

        public void CongratsOut()
        {
            m_ControllerManager.PauseGameToggle(false);
            TurnOffUi(congratsPanel);
        }

        #endregion
    }
}