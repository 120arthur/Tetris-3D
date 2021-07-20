using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Context;
using Sound;

namespace TetrisMechanic
{
    public class TetraminoManager : MonoBehaviour
    {
        // These variables define the height and width of the tetris grid.
        private const int Height = 25;
        private const int Width = 10;
        private readonly Transform[,] _grid;

        private readonly int _upperBound;

        private readonly TetraminoGrid _tetraminoGrid;

        private readonly SoundController _soundControllerInstance;

        public TetraminoManager()
        {
            _upperBound = Height - 3;
            _soundControllerInstance = SoundController.SoundControllerInstance;
            _grid = new Transform[Width, Height];
            _tetraminoGrid = new TetraminoGrid(Height, Width, _grid);
        }

        public bool ThisPositionIsValid(Transform tetramino)
        {
            foreach (Transform children in tetramino)
            {
                var position = children.transform.position;
                var roundedX = Mathf.RoundToInt(position.x);
                var roundedY = Mathf.RoundToInt(position.y);

                if (roundedX < 0 || roundedX >= Width || roundedY < 0 || roundedY >= Height ||
                    _grid[roundedX, roundedY] != null)
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
                CheckWordsInLine(line);
                ClearLine(line);
            }
        }
        public void FinishTetraminoMovement(Transform tetramino)
        {
            _tetraminoGrid.AddTetrisToPositionList(tetramino);
            IsInBound(tetramino);

            if (ContextProvider.Context.GameManager.gameIsOver) return;
            
            ContextProvider.Context.TetraminoSpawner.SpawnTetris();
            _soundControllerInstance.ChangeSfx(1);
            _soundControllerInstance.PlaySfx();
        }
        public void ClearGrid()
        {
            for (int line = 0; line < Height; line++)
            {
                _tetraminoGrid.Remove(line);
                
            } 
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

            for (var i = 0; i < Width; i++)
            {
                if (_grid[i, line].CompareTag("Untagged")) continue;
                var letterTag = _grid[i, line].tag;
                wordChar.Add(letterTag[0]);
            }

            if (ContextProvider.Context.MatchWords.VerifyMatch(wordChar))
            {
                ContextProvider.Context.GameManager.RewardChallenge();
            }

        }
        private void IsInBound(Transform block)
        {
            if (Mathf.RoundToInt(block.position.y) >= _upperBound)
            {
                ContextProvider.Context.GameManager.EndGame();
            }
        }
       
    }
}