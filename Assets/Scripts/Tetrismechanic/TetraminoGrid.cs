using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Control tetramino on the grid.
/// </summary>
public class TetraminoGrid : MonoBehaviour
{
    private const int Height = GameManager.Height;
    private const int Width = GameManager.Width;
    private static Transform[,] _grid;

    private readonly GameManager _gameManagerInstance = GameManager.GameManagerInstance;
    private readonly SoundController _soundControllerInstance = SoundController.SoundControllerInstance;

    private void Start()
    {
        _grid = _gameManagerInstance.Grid;
    }
    #region TetrisSystem
   // Verify if tetramino position is valid 
   public bool ThisPositionIsValid()
   {
       foreach (Transform children in transform)
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
   // when the tetromino finishes the move, this method adds its position on the grid. 
   public void AddTetrisToPositionList()
   {
       foreach (Transform children in transform)
       {
           var position = children.transform.position;
           _grid[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] = children;
           
           if (Mathf.RoundToInt(position.y) == Height - 3)
           {
               _gameManagerInstance.EndGame();
           }
       }

       if (_gameManagerInstance.gameIsOver) return;
       TetrisSpawner.TetrisSpawnerInstance.SpawnTetris();
       _soundControllerInstance.ChangeSfx(1);
       _soundControllerInstance.PlaySfx();
   }
  
   public void SearchForFullLines()
   {
       for (var line = Height - 1; line >= 0; line--)
       {
           if (!IsLineFull(line)) continue;
           RemoveLine(line);
           UpdateBlockLinesDown(line);
       }
   }
   
   private bool IsLineFull(int line)
   {
       List<char> wordChar = new List<char>();
       for (var column = 0; column < Width; column++)
       {
           if (_grid[column, line] == null)
           {
               return false;
           }

           if (!_grid[column, line].CompareTag("Untagged"))
           {
               var letterTag = _grid[column, line].tag;
               wordChar.Add(letterTag[0]);
           }
       }

       if (_gameManagerInstance.MatchWords.VerifyMatch(wordChar))
       {
           _gameManagerInstance.RewardChallenge();
           wordChar.Clear();
       }


       return true;
   }

   private void RemoveLine(int lineToRemove)
   {
       for (var column = 0; column < Width; column++)
       {
           if (_grid[column, lineToRemove])
               Destroy(_grid[column, lineToRemove].gameObject);
           
           _grid[column, lineToRemove] = null;
       }

       _gameManagerInstance.Score.AddPoints();
       _gameManagerInstance.gameUi.UpdateHudScore();
       _soundControllerInstance.ChangeSfx(3);
       _soundControllerInstance.PlaySfx();
   }
   //when a row is deleted, this method brings down all the tretamino blocks above it.
   private void UpdateBlockLinesDown(int deletedLine)
   {
       for (var line = deletedLine; line < Height; line++)
       {
           for (var column = 0; column < Width; column++)
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