using Score;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    public class GameScreen : Ui, IGameUI
    {
        [Inject]
        private IGameStateManager m_controllerManager;
        [Inject]
        private IScore m_iScore;

        [Header("Hud")] 
        [SerializeField] private Text m_scoreText;

        [Header("GameOver")] 
        [SerializeField] private GameOverPanel m_gameOverPanel;

        [Header("Pause")] 
        [SerializeField] private GameObject m_pausePanel;
        [SerializeField] private Button m_pauseButton;

        private void Start()
        {
            m_pauseButton.onClick.AddListener(GameOverPanelIn);
        }

        public void UpdateHudScore()
        {
            m_scoreText.text = m_iScore.GetCurrentPoints().ToString();
        }

        #region PanelsActivateAndDeactivate

        public void GameOverPanelIn()
        {
            TurnOnUi(m_gameOverPanel.gameObject);
            m_gameOverPanel.UpdateTotalScore();
        }
        public void GameOverPanelOut()
        {
            TurnOffUi(m_gameOverPanel.gameObject);
        }

        public void PauseIn()
        {
            m_controllerManager.ChangeState(GameState.PAUSE);
            TurnOnUi(m_pausePanel);
        }

        public void PauseOut()
        {
            m_controllerManager.ChangeState(GameState.CONTUNUE);
            TurnOffUi(m_pausePanel);
        }
        #endregion
    }
}

