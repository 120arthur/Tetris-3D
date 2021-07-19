using Score;
using Tetrismechanic;
namespace Context
{
    public class Context : IContext
    {
        public AssetLoader AssetLoader { get; }
        public GameManager GameManager { get; }
        public IScore Score { get; }
        public IMatchWords MatchWords { get; }
        public WordsScriptable WordsScriptable { get; }
        public TetraminoManager TetraminoManager { get; }
        public TetraminoSpawner TetraminoSpawner { get;}

        public Context()
        {
            ContextProvider.Subscribe(this);

            AssetLoader = new AssetLoader();

            GameManager = AssetLoader.LoadAndInstantiate<GameManager>($"Managers/{nameof(GameManager)}");

            WordsScriptable = AssetLoader.Load<WordsScriptable>($"WordSetting/{nameof(WordsScriptable)}");

            TetraminoSpawner = AssetLoader.LoadAndInstantiate<TetraminoSpawner>($"Managers/{nameof(TetraminoSpawner)}");

            TetraminoManager = new TetraminoManager();
            Score = new ScoreController();
            MatchWords = new MatchWords(WordsScriptable.word);

        }
    }
}