using UnityEngine;

namespace Tetrismechanic
{
    public interface ITetraminoGrid
    {
        bool ThisPositionIsValid(Transform tetramino);
        void AddTetrisToPositionList(Transform tetramino);
        void SearchForFullLines();
        bool IsLineFull(int line);
        void RemoveLine(int lineToRemove);
        void UpdateBlockLinesDown(int deletedLine);
    }
}