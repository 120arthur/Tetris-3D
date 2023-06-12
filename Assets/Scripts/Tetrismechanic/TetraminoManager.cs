using System.Collections.Generic;
using UnityEngine;
using Sound;
using Zenject;
using Match;
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
        private MatchWords m_MatchWords;
        [Inject]
        private ControllerManager m_ControllerManager;
        [Inject]
        private TetraminoSpawner m_TetraminoSpawner;
        [Inject]
        private ScoreManager m_ScoreManager;

        // These variables define the height and width of the tetris grid.
        private const int m_Height = 25;
        private const int m_Width = 10;
        private readonly Transform[,] _grid;

        private readonly int m_upperBound;

        private readonly TetraminoGrid m_tetraminoGrid;

        public TetraminoManager()
        {
            m_upperBound = m_Height - 3;
            _grid = new Transform[m_Width, m_Height];
            m_tetraminoGrid = new TetraminoGrid(m_Height, m_Width, _grid);
        }

        public bool ThisPositionIsValid(Transform tetramino)
        {
            foreach (Transform children in tetramino)
            {
                var position = children.transform.position;
                var roundedX = Mathf.RoundToInt(position.x);
                var roundedY = Mathf.RoundToInt(position.y);

                if (roundedX < 0 || roundedX >= m_Width || roundedY < 0 || roundedY >= m_Height ||
                    _grid[roundedX, roundedY] != null)
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
                CheckWordsInLine(line);
                ClearLine(line);
            }
        }
        public void FinishTetraminoMovement(Transform tetramino)
        {
            m_tetraminoGrid.AddTetrisToPositionList(tetramino);
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
                m_tetraminoGrid.Remove(line);

            }
        }

        private void ClearLine(int line)
        {
            m_tetraminoGrid.Remove(line);
            m_tetraminoGrid.UpdateBlockLinesDown(line);

            m_ScoreManager.AddPoints();
            m_GameUi.UpdateHudScore();
            m_SoundController.ChangeSfx(3);
            m_SoundController.PlaySfx();
        }
        private bool IsLineFull(int line)
        {

            for (var column = 0; column < m_Width; column++)
            {
                if (_grid[column, line] == null)
                {
                    return false;
                }
            }
            return true;
        }
        private void CheckWordsInLine(int line)
        {
            if (!IsLineFull(line)) return;

            var wordChar = new List<char>();

            for (var i = 0; i < m_Width; i++)
            {
                if (_grid[i, line].CompareTag("Untagged")) continue;
                var letterTag = _grid[i, line].tag;
                wordChar.Add(letterTag[0]);
            }

            if (m_MatchWords.VerifyMatch(wordChar))
            {
                m_ControllerManager.RewardChallenge();
            }

        }
        private void IsInBound(Transform block)
        {
            if (Mathf.RoundToInt(block.position.y) >= m_upperBound)
            {
                m_ControllerManager.EndGame();
            }
        }

    }
}