using UnityEngine;

namespace Tetrismechanic
{
    public interface ITetraminoGrid
    {
        void AddTetrisToPositionList(Transform tetramino);
        void Remove(int lineToRemove);
        void UpdateBlockLinesDown(int deletedLine);
    }
}