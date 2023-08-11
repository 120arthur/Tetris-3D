using Score;
using Sound;
using TetrisMechanic;
using Ui;
using UnityEngine;
using Zenject;

public enum GameState
{
    START,
    PAUSE,
    CONTUNUE,
    REPLAY,
    GAMEOVER,
    BACKTOMENU
}

public class GameStateManager : IGameStateManager
{
    private const string MENU_SCENE_NAME = "Menu";

    [Inject]
    ISoundController m_soundController;
    [Inject]
    private IGameUI m_gameUi;
    [Inject]
    private ITetraminoController m_tetraminoManager;
    [Inject]
    private ITetraminoSpawner m_tetraminoSpawner;
    [Inject]
    private IScore m_iScore;
    [Inject]
    private IGameManager m_gameManager;

    private bool m_gameIsOver;

    private GameState m_currentState;

    public void ChangeState(GameState gameState)
    {
            m_currentState = gameState;

            switch (gameState)
            {
                case GameState.START:
                    StartGame();
                    break;
                case GameState.PAUSE:
                    PauseGameToggle(true);
                    break;
                case GameState.CONTUNUE:
                    PauseGameToggle(false);
                    break;
                case GameState.REPLAY:
                    Replay();
                    break;
                case GameState.GAMEOVER:
                    EndGame();
                    break;
                case GameState.BACKTOMENU:
                    LoadMenu();
                    break;
                default:
                    break;
            }
    }

    private void StartGame()
    {
        m_gameIsOver = false;
        m_soundController.ChangeMusic(1);
    }

    private void PauseGameToggle(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }

    private void EndGame()
    {
        PauseGameToggle(true);
        m_gameIsOver = true;
        m_gameUi.GameOverPanelIn();
        m_soundController.ChangeSfx(2);
        m_soundController.PlaySfx();
    }

    private void Replay()
    {
        PauseGameToggle(false);
        m_tetraminoSpawner.DestroyCurrentTetramino();
        m_tetraminoManager.ClearGrid();
        m_iScore.RemovePoints();
        m_gameUi.UpdateHudScore();
        m_tetraminoSpawner.SpawnTetris();
        m_gameIsOver = false;
    }

    private void LoadMenu()
    {
        PauseGameToggle(false);
        m_gameManager.ChangeScene(MENU_SCENE_NAME);
    }

    public bool GameIsOver()
    {
        return m_gameIsOver;
    }
}