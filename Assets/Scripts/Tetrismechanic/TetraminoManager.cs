using UnityEngine;
using Sound;
using Zenject;
using Score;
using Ui;

namespace TetrisMechanic
{
    public class TetraminoManager : MonoBehaviour
    {
        [Inject]
        private SoundController m_SoundController;
        [Inject]
        private GameUi m_GameUi;
        [Inject]
        private ControllerManager m_ControllerManager;
        [Inject]
        private TetraminoSpawner m_TetraminoSpawner;
        [Inject]
        private ScoreManager m_ScoreManager;

        // These variables define the height and width of the tetris grid.
        private const int m_Height = 25;
        private const int m_Width = 10;
        private readonly Transform[,] m_Grid;

        private readonly int m_UpperBound;

        private readonly TetraminoGrid m_TetraminoGrid;

        public TetraminoManager()
        {
            m_UpperBound = m_Height - 3;
            m_Grid = new Transform[m_Width, m_Height];
            m_TetraminoGrid = new TetraminoGrid(m_Height, m_Width, m_Grid);
        }

        public bool ThisPositionIsValid(Transform tetramino)
        {
            foreach (Transform children in tetramino)
            {
                var position = children.transform.position;
                var roundedX = Mathf.RoundToInt(position.x);
                var roundedY = Mathf.RoundToInt(position.y);

                if (roundedX < 0 || roundedX >= m_Width || roundedY < 0 || roundedY >= m_Height ||
                    m_Grid[roundedX, roundedY] != null)
                {
                    return false;
                }
            }

            return true;
        }
        public void SearchForFullLines()
        {
            for (var line = m_Height - 1; line >= 0; line--)
            {
                if (!IsLineFull(line)) continue;
                ClearLine(line);
            }
        }

        public void SearchForSpecialCubesMatch()
        {
            for (var line = m_Height - 1; line >= 0; line--)
            {
                if (!IsLineFull(line)) continue;
                ClearLine(line);
            }
        }

        public void FinishTetraminoMovement(Transform tetramino)
        {
            m_TetraminoGrid.AddTetrisToPositionList(tetramino);
            IsInBound(tetramino);

            if (m_ControllerManager.GameIsOver) return;

            m_TetraminoSpawner.SpawnTetris();
            m_SoundController.ChangeSfx(1);
            m_SoundController.PlaySfx();
        }
        public void ClearGrid()
        {
            for (int line = 0; line < m_Height; line++)
            {
                m_TetraminoGrid.Remove(line);

            }
        }

        private void ClearLine(int line)
        {
            m_TetraminoGrid.Remove(line);
            m_TetraminoGrid.UpdateBlockLinesDown(line);

            m_ScoreManager.AddPoints();
            m_GameUi.UpdateHudScore();
            m_SoundController.ChangeSfx(3);
            m_SoundController.PlaySfx();
        }

        private bool IsLineFull(int line)
        {

            for (var column = 0; column < m_Width; column++)
            {
                if (m_Grid[column, line] == null)
                {
                    return false;
                }
            }
            return true;
        }

        private void GetSpecialCubesOnLine()
        {

        }

        private void IsInBound(Transform block)
        {
            if (Mathf.RoundToInt(block.position.y) >= m_UpperBound)
            {
                m_ControllerManager.EndGame();
            }
        }

    }
}