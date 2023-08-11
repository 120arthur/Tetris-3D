public interface IGameStateManager
{
    void ChangeState(GameState gameState);
    bool GameIsOver();
}