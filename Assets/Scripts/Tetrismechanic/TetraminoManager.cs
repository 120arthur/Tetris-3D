using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Context;

namespace Tetrismechanic
{


    public class TetraminoManager : MonoBehaviour
    {
        // These variables define the height and width of the tetris grid.
        public readonly int Height = 25;
        public readonly int Width = 10;
        public readonly Transform[,] Grid;

        private readonly int upperBound;

        private ITetraminoGrid _tetraminoGrid;

        private SoundController _soundControllerInstance;

        public TetraminoManager()
        {
            upperBound = Height - 3;
            _soundControllerInstance = SoundController.SoundControllerInstance;
            Grid = new Transform[Width, Height];
            _tetraminoGrid = new TetraminoGrid(Height, Width, Grid);
        }

        public bool ThisPositionIsValid(Transform tetramino)
        {
            foreach (Transform children in tetramino)
            {
                var position = children.transform.position;
                var roundedX = Mathf.RoundToInt(position.x);
                var roundedY = Mathf.RoundToInt(position.y);

                if (roundedX < 0 || roundedX >= Width || roundedY < 0 || roundedY >= Height ||
                    Grid[roundedX, roundedY] != null)
                {
                    return false;
                }
            }

            return true;
        }
        public void SearchForFullLines()
        {
            for (var line = Height - 1; line >= 0; line--)
            {
                if (!IsLineFull(line)) continue;
                checkWordsInLine(line);
                ClearLine(line);
            }
        }
        public void FinishTetraminoMovment(Transform tetramino)
        {
            _tetraminoGrid.AddTetrisToPositionList(tetramino);
            IsInBound(tetramino);

            if (ContextProvider.Context.GameManager.gameIsOver) return;

            TetrisSpawner.TetrisSpawnerInstance.SpawnTetris();
            _soundControllerInstance.ChangeSfx(1);
            _soundControllerInstance.PlaySfx();
        }

        private void ClearLine(int line)
        {
            _tetraminoGrid.Remove(line);
            _tetraminoGrid.UpdateBlockLinesDown(line);

            ContextProvider.Context.Score.AddPoints();
            ContextProvider.Context.GameManager.gameUi.UpdateHudScore();
            _soundControllerInstance.ChangeSfx(3);
            _soundControllerInstance.PlaySfx();
        }
        private bool IsLineFull(int line)
        {

            for (var column = 0; column < Width; column++)
            {
                if (Grid[column, line] == null)
                {
                    return false;
                }
            }
            return true;
        }
        private void checkWordsInLine(int line)
        {
            if (!IsLineFull(line)) return;

            var wordChar = new List<char>();

            for (int i = 0; i < Width; i++)
            {
                if (!Grid[i, line].CompareTag("Untagged"))
                {
                    var letterTag = Grid[i, line].tag;
                    wordChar.Add(letterTag[0]);
                }
            }

            if (ContextProvider.Context.MatchWords.VerifyMatch(wordChar))
            {
                ContextProvider.Context.GameManager.RewardChallenge();
                wordChar.Clear();
            }

        }
        private void IsInBound(Transform block)
        {
            if (Mathf.RoundToInt(block.position.y) >= upperBound)
            {
                ContextProvider.Context.GameManager.EndGame();
            }
        }
    }
}