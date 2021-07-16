using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Context;

public class GameOverPanel : Ui
{
    [SerializeField] private Text _totalScore;

    public void UpdateTotalScore()
    {
        _totalScore.text = ContextProvider.Context.Score.CurrentPoints().ToString();
    }

    public void Show()
    {
       TurnOnUi(gameObject); 
    }
}
