using UnityEngine;
using Sound;
using Zenject;
using Score;
using Ui;
using System;
using Input;

namespace TetrisMechanic
{
    public class TetraminoController
    {
        [Inject]
        private SoundController m_soundController;
        [Inject]
        private GameUi m_gameUi;
        [Inject]
        private ControllerManager m_controllerManager;
        [Inject]
        private TetraminoSpawner m_tetraminoSpawner;
        [Inject]
        private IScore m_iScore;
        [Inject]
        private SignalBus m_signalBus;

        // These variables define the height and width of the tetris grid.
        private const int m_height = 25;
        private const int m_width = 10;
        private GridPosInfo[,] m_grid;

        private int m_upperBound;

        private TetraminoGrid m_tetraminoGrid;

        [Inject]
        private void Initialize()
        {
            SubscribeSignals();
            m_upperBound = m_height - 3;
            m_grid = new GridPosInfo[m_width, m_height];
            m_tetraminoGrid = new TetraminoGrid(m_height, m_width, m_grid);
        }

        public bool ThisPositionIsValid(TetraminoCube[] m_tetraminoCubes, MovementType movementType)
        {
            foreach (TetraminoCube cube in m_tetraminoCubes)
            {
                var position = cube.transform.position;
                var roundedX = Mathf.RoundToInt(position.x);
                var roundedY = Mathf.RoundToInt(position.y);

                if (roundedX < 0 || roundedX >= m_width || roundedY < 0 || roundedY >= m_height ||
                    m_grid[roundedX, roundedY] != null)
                {
                    return false;
                }
            }

            return true;
        }
        public void SearchForFullLines(OnTetratiminoFinishMovementSignal args)
        {
            for (var line = m_height - 1; line >= 0; line--)
            {
                if (!IsLineFull(line)) continue;
                ClearLine(line);
            }
        }

        public void FinishTetraminoMovement(OnTetratiminoFinishMovementSignal args)
        {
            m_tetraminoGrid.AddTetrisToPositionList(args.m_tetraminoCubes);
            IsInBound(args.m_tetraminoCubes);

            if (m_controllerManager.m_gameIsOver) return;

            m_tetraminoSpawner.SpawnTetris();
            m_soundController.ChangeSfx(1);
            m_soundController.PlaySfx();
        }
        public void ClearGrid()
        {
            for (int line = 0; line < m_height; line++)
            {
                m_tetraminoGrid.Remove(line);
            }
        }

        private void ClearLine(int line)
        {
            m_tetraminoGrid.Remove(line);
            m_tetraminoGrid.UpdateBlockLinesDown(line);

            m_iScore.AddPoints();
            m_gameUi.UpdateHudScore();
            m_soundController.ChangeSfx(3);
            m_soundController.PlaySfx();
        }

        private bool IsLineFull(int line)
        {
            for (var column = 0; column < m_width; column++)
            {
                if (m_grid[column, line] == null)
                {
                    return false;
                }
            }
            return true;
        }

        private void IsInBound(TetraminoCube[] m_tetraminoCubes)
        {
            foreach (TetraminoCube cube in m_tetraminoCubes)
            {
                if (Mathf.RoundToInt(cube.transform.position.y) >= m_upperBound)
                {
                    m_controllerManager.EndGame();
                    return;
                }
            }
        }
        private void SubscribeSignals()
        {
            m_signalBus.Subscribe<OnTetratiminoFinishMovementSignal>(FinishTetraminoMovement);
            m_signalBus.Subscribe<OnTetratiminoFinishMovementSignal>(SearchForFullLines);
            m_signalBus.Subscribe<OnGameEndSignal>(UnsubscribeSignals);
        }

        private void UnsubscribeSignals()
        {
            m_signalBus.TryUnsubscribe<OnTetratiminoFinishMovementSignal>(FinishTetraminoMovement);
            m_signalBus.TryUnsubscribe<OnTetratiminoFinishMovementSignal>(SearchForFullLines);
            m_signalBus.TryUnsubscribe<OnGameEndSignal>(UnsubscribeSignals);
        }

    }
}

public class GridPosInfo
{
    private Transform m_tetraminoTransform;
    private Guid m_tetraminoId;

    public Transform TetraminoTransform => m_tetraminoTransform;
    public Guid TetraminoId => m_tetraminoId;

    public void SetNewTetris(Transform tetraminoTransform, Guid tetraminoGuid)
    {
        m_tetraminoTransform = tetraminoTransform;
        m_tetraminoId = tetraminoGuid;
    }

    public void RemoveTetris()
    {
        m_tetraminoTransform = null;
        m_tetraminoId = Guid.Empty;
    }

    public bool HasCube()
    {
        return m_tetraminoTransform != null && m_tetraminoId != Guid.Empty ? true : false;
    }
}