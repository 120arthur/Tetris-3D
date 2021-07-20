using Score;
using Tetrismechanic;
namespace Context
{
    public class Context : IContext
    {
        private AssetLoader AssetLoader { get; }

        public GameManager GameManager { get; }
        public WordsScriptable WordsScriptable { get; }
        public TetraminoManager TetraminoManager { get; }
        public TetraminoSpawner TetraminoSpawner { get;}
        public IScore Score { get; }
        public IMatchWords MatchWords { get; }
        public IInputType InputType { get; }

        public Context()
        {
            ContextProvider.Subscribe(this);

            AssetLoader = new AssetLoader();

            GameManager = AssetLoader.LoadAndInstantiate<GameManager>($"Managers/{nameof(GameManager)}");

            WordsScriptable = AssetLoader.Load<WordsScriptable>($"WordSetting/{nameof(WordsScriptable)}");

            TetraminoSpawner = AssetLoader.LoadAndInstantiate<TetraminoSpawner>($"Managers/{nameof(TetraminoSpawner)}");

            InputType = new DesktopInputType();
            TetraminoManager = new TetraminoManager();
            Score = new ScoreManager();
            MatchWords = new MatchWords(WordsScriptable.Word);

        }
    }
}