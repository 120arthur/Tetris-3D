using Tetrismechanic;

namespace Context
{
    public interface IContext 
    {
        public GameManager GameManager { get; }
        public  TetraminoManager TetraminoManager { get; }
        public TetraminoSpawner TetraminoSpawner { get; }
        public IInputType InputType { get; }
        public IScore Score { get; }
        public IMatchWords MatchWords { get; }
    }
}
