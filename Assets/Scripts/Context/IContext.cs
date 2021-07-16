namespace Context
{
    public interface IContext 
    {
        public AssetLoader AssetLoader { get; }
        public GameManager GameManager { get; }
        public IScore Score { get; }
        public IMatchWords MatchWords { get; }  
    
    }
}
