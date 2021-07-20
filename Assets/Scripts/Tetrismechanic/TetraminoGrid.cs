using UnityEngine;

namespace TetrisMechanic
{
    /// <summary>
    /// Puts and removes tetraminos in the grid
    /// </summary>
    public class TetraminoGrid : MonoBehaviour
    {
        private readonly int _height;
        private readonly int _width;
        private readonly Transform[,] _grid;

        public TetraminoGrid(int height, int width, Transform[,] grid)
        {
            _height = height;
            _width = width;
            _grid = grid;
        }

        /// <summary>
        // when the tetromino finishes the move, this method adds its position on the grid.
        /// </summary>
        /// <param name="tetramino"></param>
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
                if (!_grid[column, lineToRemove]) continue;
                Destroy(_grid[column, lineToRemove].gameObject);
                _grid[column, lineToRemove] = null;
            }
        }

        /// <summary>
        //when a row is deleted, this method brings down all the tretamino blocks above it.
        /// </summary>
        /// <param name="deletedLine"></param>
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
    }
}