using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    public class PausePanel : Ui
    {
        [Inject]
        private IGameStateManager m_controllerManager;
        [Inject]
        private IGameUI m_gameUi;

        [SerializeField] private Button m_continueButton;
        [SerializeField] private Button m_menuButton;
        [SerializeField] private Button m_replayButton;

        private void Awake()
        {
            m_continueButton.onClick.AddListener(m_gameUi.PauseOut);

            m_menuButton.onClick.AddListener(() => m_controllerManager.ChangeState(GameState.BACKTOMENU));

            m_replayButton.onClick.AddListener(() =>
                                             {
                                                 m_controllerManager.ChangeState(GameState.REPLAY);
                                                 m_gameUi.PauseOut();
                                             });
        }
    }
}