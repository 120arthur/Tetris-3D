using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;

    // These variables define the height and width of the tetris grid.
    public const int Height = 25;
    public const int Width = 10;
    public readonly Transform[,] Grid = new Transform[Width, Height];

    [HideInInspector] public bool gameIsOver;

    public GameUi gameUi;

    public IScore Score;
    public IMatchWords MatchWords;

    private void Awake()
    {
        GameManagerInstance = this;
        Score = GetComponent<ScoreController>();
        MatchWords = GetComponent<MatchWords>();
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
        Time.timeScale = pause == true ? 0 : 1;
    }

    /// <summary>
    /// This class calls the methods that end the game.
    /// </summary>
    public void EndGame()
    {
        gameIsOver = true;
        gameUi.GameOverPanelIn();
        SoundController.SoundControllerInstance.ChangeSfx(2);
        SoundController.SoundControllerInstance.PlaySfx();
    }

    /// <summary>
    /// This class calls the methods that reward the player. 
    /// </summary>
    public void RewardChallenge()
    {
        gameUi.CongratsIn();
        Score.RewardPoints();
    }
}