using Score;
using Sound;
using TetrisMechanic;
using Ui;
using UnityEngine;
using Zenject;

public class ControllerManager : MonoBehaviour
{
    [Inject]
    SoundController m_SoundController;
    [Inject]
    private GameUi m_GameUi;
    [Inject]
    private TetraminoManager m_TetraminoManager;
    [Inject]
    private TetraminoSpawner m_TetraminoSpawner;   
    [Inject]
    private ScoreManager m_ScoreManager;    

    [HideInInspector]
    public bool GameIsOver;

    private void Start()
    {
        m_SoundController.ChangeMusic(1);
        m_SoundController.PlayMusic();
    }

    public void PauseGameToggle(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }

    public void EndGame()
    {
        PauseGameToggle(true);
        GameIsOver = true;
        m_GameUi.GameOverPanelIn();
        m_SoundController.ChangeSfx(2);
        m_SoundController.PlaySfx();
    }

    public void Replay()
    {
        PauseGameToggle(false);
        Destroy(m_TetraminoSpawner.m_CurrentTetramino);
        m_TetraminoManager.ClearGrid();
        m_ScoreManager.RemovePoints();
        m_GameUi.UpdateHudScore();
        m_TetraminoSpawner.SpawnTetris();
        GameIsOver = false;
    }

    public void LoadMenu()
    {
        PauseGameToggle(false);
        m_GameUi.ChangeScene("Menu");
    }

    public void RewardChallenge()
    {
        m_GameUi.CongratsIn();
        m_ScoreManager.RewardPoints();
    }
}