using Context;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class PausePanel : Ui
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button replayButton;

        private void Awake()
        {
            continueButton.onClick.AddListener(() => { ContextProvider.Context.GameManager.gameUi.PauseOut(); });

            menuButton.onClick.AddListener(() => { ContextProvider.Context.GameManager.LoadMenu(); });
            
            replayButton.onClick.AddListener(() =>
                                             {
                                                 ContextProvider.Context.GameManager.Replay();
                                                 ContextProvider.Context.GameManager.gameUi.PauseOut();
                                             });
            
        }
    }
}