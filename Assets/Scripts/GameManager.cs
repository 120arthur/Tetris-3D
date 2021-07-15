using Context;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // These variables define the height and width of the tetris grid.
    public const int Height = 25;
    public const int Width = 10;
    public readonly Transform[,] Grid = new Transform[Width, Height];

    [HideInInspector] public bool gameIsOver;

    public IScore Score;
    public IMatchWords MatchWords;

    private void Awake()
    {
        Score = GetComponent<IScore>();
        MatchWords = GetComponent<IMatchWords>();
    }

    private void Start()
    {
        SoundController.SoundControllerInstance.ChangeMusic(1);
        SoundController.SoundControllerInstance.PlayMusic();
    }

    /// <summary>
    /// Using TimeScale, this class stops or continues the game.
    /// </summary>
    /// <param name="pause"></param>
    public void PauseGameToggle(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }

    /// <summary>
    /// This class calls the methods that end the game.
    /// </summary>
    public void EndGame()
    {
        gameIsOver = true;
        ContextProvider.Context.gameUi.GameOverPanelIn();
        SoundController.SoundControllerInstance.ChangeSfx(2);
        SoundController.SoundControllerInstance.PlaySfx();
    }

    /// <summary>
    /// This class calls the methods that reward the player. 
    /// </summary>
    public void RewardChallenge()
    {
        ContextProvider.Context.gameUi.CongratsIn();
        Score.RewardPoints();
    }
}