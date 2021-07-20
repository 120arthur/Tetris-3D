using System;
using Context;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameOverPanel : Ui
    {
        [SerializeField] private Text totalScore;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button replayButton;

        private void Awake()
        {
            menuButton.onClick.AddListener(() => { ContextProvider.Context.GameManager.LoadMenu(); });
            
            replayButton.onClick.AddListener(() =>
                                             {
                                                 ContextProvider.Context.GameManager.Replay();
                                                 ContextProvider.Context.GameManager.gameUi.GameOverPanelOut();
                                             });
            
        }
        public void UpdateTotalScore()
        {
            totalScore.text = ContextProvider.Context.Score.CurrentPoints().ToString();
        }
        
    }
}