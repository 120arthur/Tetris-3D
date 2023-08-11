namespace TetrisMechanic
{
    public interface ITetraminoController
    {
        void ClearGrid();
        void FinishTetraminoMovement(OnTetratiminoFinishMovementSignal args);
        void SearchForFullLines(OnTetratiminoFinishMovementSignal args);
        bool ThisPositionIsValid(TetraminoCube[] m_tetraminoCubes);
    }
}