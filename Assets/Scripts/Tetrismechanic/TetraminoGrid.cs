using System.Collections.Generic;
using UnityEngine;
using Context;

namespace Tetrismechanic
{
    /// <summary>
    /// puts and removes tetraminos in the grid
    /// </summary>
    public class TetraminoGrid : MonoBehaviour
    {
        private int _height;
        private int _width;
        private Transform[,] _grid;

        public TetraminoGrid(int height, int width, Transform[,] grid)
        {
            _height = height;
            _width = width;
            _grid = grid;
        }

        #region TetrisSystem

        // when the tetromino finishes the move, this method adds its position on the grid. 
        public void AddTetrisToPositionList(Transform tetramino)
        {
            foreach (Transform children in tetramino)
            {
                var position = children.transform.position;
                _grid[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] = children;
            }
        }

        public void Remove(int lineToRemove)
        {
            for (var column = 0; column < _width; column++)
            {
                if (_grid[column, lineToRemove])
                {
                    Destroy(_grid[column, lineToRemove].gameObject);
                    _grid[column, lineToRemove] = null;
                }
            }
        }

        //when a row is deleted, this method brings down all the tretamino blocks above it.
        public void UpdateBlockLinesDown(int deletedLine)
        {
            for (var line = deletedLine; line < _height; line++)
            {
                for (var column = 0; column < _width; column++)
                {
                    if (_grid[column, line] == null) continue;
                    _grid[column, line - 1] = _grid[column, line];
                    _grid[column, line] = null;
                    _grid[column, line - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }

        #endregion
    }
}