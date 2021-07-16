namespace Tetrismechanic
{
    public interface ITetraminoGrid
    {
        bool ThisPositionIsValid();
        void AddTetrisToPositionList();
        void SearchForFullLines();
        bool IsLineFull(int line);
        void RemoveLine(int lineToRemove);
        void UpdateBlockLinesDown(int deletedLine);
    }
}