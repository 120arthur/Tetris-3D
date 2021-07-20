using Context;
using Sound;
using Ui;
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
        PauseGameToggle(true);
        gameIsOver = true;
        gameUi.GameOverPanelIn();
        SoundController.SoundControllerInstance.ChangeSfx(2);
        SoundController.SoundControllerInstance.PlaySfx();
    }

    public void Replay()
    {
        PauseGameToggle(false);
        Destroy(ContextProvider.Context.TetraminoSpawner.currentTetramino);
        ContextProvider.Context.TetraminoManager.ClearGrid();
        ContextProvider.Context.Score.RemovePoints();
        ContextProvider.Context.GameManager.gameUi.UpdateHudScore();
        ContextProvider.Context.TetraminoSpawner.SpawnTetris();
        gameIsOver = false;
    }

    public void LoadMenu()
    {
        PauseGameToggle(false);
        gameUi.ChangeScene("Menu");
    }

    public void RewardChallenge()
    {
        gameUi.CongratsIn();
        ContextProvider.Context.Score.RewardPoints();
    }
}