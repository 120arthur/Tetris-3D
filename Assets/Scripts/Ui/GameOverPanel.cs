using Score;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    public class GameOverPanel : Ui
    {
        [Inject]
        private ControllerManager m_ControllerManager;
        [Inject]
        private ScoreManager m_ScoreManager;
        [Inject]
        private GameUi m_GameUi;

        [SerializeField] private Text totalScore;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button replayButton;

        private void Awake()
        {
            menuButton.onClick.AddListener(() => { m_ControllerManager.LoadMenu(); });

            replayButton.onClick.AddListener(() =>
                                             {
                                                 m_ControllerManager.Replay();
                                                 m_GameUi.GameOverPanelOut();
                                             });
        }
        public void UpdateTotalScore()
        {
            totalScore.text = m_ScoreManager.CurrentPoints().ToString();
        }
        
    }
}