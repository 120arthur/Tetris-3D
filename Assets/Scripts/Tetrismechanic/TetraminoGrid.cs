using UnityEngine;

namespace TetrisMechanic
{
    /// <summary>
    /// Puts and removes tetraminos in the grid
    /// </summary>
    public class TetraminoGrid : MonoBehaviour
    {
        private readonly int m_height;
        private readonly int m_width;
        private readonly GridPosInfo[,] m_grid;

        public TetraminoGrid(int height, int width, GridPosInfo[,] grid)
        {
            m_height = height;
            m_width = width;
            m_grid = grid;
        }

        /// <summary>
        // when the tetromino finishes the move, this method adds its position on the grid.
        /// </summary>
        /// <param name="tetramino"></param>
        public void AddTetrisToPositionList(TetraminoCube[] tetraminoCubes)
        {
            foreach (TetraminoCube cube in tetraminoCubes)
            {
                Vector3 position = cube.transform.position;

                int posX = Mathf.RoundToInt(position.x);
                int posY = Mathf.RoundToInt(position.y);
                
                if (m_grid[posX, posY] == null)
                {
                    m_grid[posX, posY] = new GridPosInfo();
                }

                m_grid[posX, posY].SetNewTetris(cube.transform, cube.ID);
            }
        }

        public void Remove(int lineToRemove)
        {
            for (var column = 0; column < m_width; column++)
            {
                if (m_grid[column, lineToRemove].HasCube() == false)
                {
                    continue;
                }
                Destroy(m_grid[column, lineToRemove].TetraminoTransform.gameObject);
                m_grid[column, lineToRemove].RemoveTetris();
            }
        }

        /// <summary>
        //when a row is deleted, this method brings down all the tretamino blocks above it.
        /// </summary>
        /// <param name="deletedLine"></param>
        public void UpdateBlockLinesDown(int deletedLine)
        {
            for (var line = deletedLine; line < m_height; line++)
            {
                for (var column = 0; column < m_width; column++)
                {
                    if (m_grid[column, line] == null || m_grid[column, line].HasCube() == false) continue;
                    m_grid[column, line - 1].SetNewTetris(m_grid[column, line].TetraminoTransform, m_grid[column, line].TetraminoId);
                    m_grid[column, line].RemoveTetris();
                    m_grid[column, line - 1].TetraminoTransform.position = Vector3.down;
                }
            }
        }
    }
}