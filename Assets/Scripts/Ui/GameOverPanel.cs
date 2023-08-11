using Score;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    public class GameOverPanel : Ui
    {
        [Inject]
        private IGameStateManager m_controllerManager;
        [Inject]
        private IScore m_iScore;
        [Inject]
        private IGameUI m_gameUi;

        [SerializeField] private Text m_totalScore;
        [SerializeField] private Button m_menuButton;
        [SerializeField] private Button m_replayButton;

        private void Awake()
        {
            m_menuButton.onClick.AddListener(() => m_controllerManager.ChangeState(GameState.BACKTOMENU));

            m_replayButton.onClick.AddListener(() =>
                                             {
                                                 m_controllerManager.ChangeState(GameState.REPLAY);
                                                 m_gameUi.GameOverPanelOut();
                                             });
        }

        public void UpdateTotalScore()
        {
            m_totalScore.text = m_iScore.GetCurrentPoints().ToString();
        }
        
    }
}