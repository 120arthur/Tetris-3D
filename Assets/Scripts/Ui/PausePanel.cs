using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    public class PausePanel : Ui
    {
        [Inject]
        private ControllerManager m_ControllerManager;
        [Inject]
        private GameUi m_GameUi;

        [SerializeField] private Button continueButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button replayButton;

        private void Awake()
        {
            continueButton.onClick.AddListener(() => { m_GameUi.PauseOut(); });

            menuButton.onClick.AddListener(() => { m_ControllerManager.LoadMenu(); });

            replayButton.onClick.AddListener(() =>
                                             {
                                                 m_ControllerManager.Replay();
                                                 m_GameUi.PauseOut();
                                             });
        }
    }
}