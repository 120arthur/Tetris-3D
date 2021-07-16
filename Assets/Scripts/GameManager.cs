using Context;
using Tetrismechanic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool gameIsOver;

    public GameUi gameUi;

    private void Start()
    {
        SoundController.SoundControllerInstance.ChangeMusic(1);
        SoundController.SoundControllerInstance.PlayMusic();
    }

    public void PauseGameToggle(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }

    public void EndGame()
    {
        gameIsOver = true;
        gameUi.GameOverPanelIn();
        SoundController.SoundControllerInstance.ChangeSfx(2);
        SoundController.SoundControllerInstance.PlaySfx();
    }

    public void RewardChallenge()
    {
        gameUi.CongratsIn();
        ContextProvider.Context.Score.RewardPoints();
    }
}