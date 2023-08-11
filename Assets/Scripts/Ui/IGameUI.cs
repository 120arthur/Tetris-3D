namespace Ui
{
    public interface IGameUI
    {
        void GameOverPanelIn();
        void GameOverPanelOut();
        void PauseIn();
        void PauseOut();
        void UpdateHudScore();
    }
}