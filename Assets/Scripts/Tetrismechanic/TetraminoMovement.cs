using System.Collections;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Input;
using Score;
using Sound;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using System;

namespace TetrisMechanic
{
    public class TetraminoMovement : MonoBehaviour
    {
        [Inject]
        private ISoundController m_soundController;
        [Inject]
        private ITetraminoController m_tetraminoManager;
        [Inject]
        private ITetraminoSpawner m_tetraminoSpawner;
        [Inject]
        private IScore m_iScore;
        [Inject]
        private IInputType m_iInputType;
        [Inject]
        private SignalBus m_signalBus;

        [SerializeField] private TetraminoCube[] m_tetraminoCubes;

        private Guid m_id;

        private float m_fastSpeed = 0.3f;
        private float m_normalSpeed = 0.7f;

        private float m_currentSpeed;

        private bool m_canMove = true;

        private GameObject m_parentAnchor;

        public GameObject ParentAnchor => m_parentAnchor;
        public void InitTetramino()
        {
            SetTetraminoCubes();
            m_parentAnchor = transform.parent.gameObject;
            SetVelocity();
            m_currentSpeed = m_normalSpeed;
            StartCoroutine(TetraminoFall());

            m_id = Guid.NewGuid();
            SetIDs(m_id);
        }

        private void Update()
        {
            InputVerifier();
        }

        private void OnCubeTypesLadComplete(AsyncOperationHandle<IList<TetraminoCubeInfo>> asyncOperationHandle)
        {
            Debug.Log("AsyncOperationHandle Status: " + asyncOperationHandle.Status);

            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
            }
        }

        private void SetTetraminoCubes()
        {
            float random = UnityEngine.Random.Range(1f, 100f);
            if (random < 80)
            {
                m_tetraminoCubes[UnityEngine.Random.Range(1, m_tetraminoCubes.Length)].SetTetraminoCubeInfo(SortSpecialCube());
            }

            for (int i = 0; i < m_tetraminoCubes.Length; i++)
            {
                if (m_tetraminoCubes[i].TetraminoCubeType == TetraminoCubeType.NORMAL)
                {
                    m_tetraminoCubes[i].SetTetraminoCubeInfo(SetNormalCube());
                }
            }
        }

        private TetraminoCubeInfo SortSpecialCube()
        {
            IList<TetraminoCubeInfo> itens = GetCubeInfo();
            if (itens != null)
            {
                int sortedIndex = UnityEngine.Random.Range(0, itens.Count);
                TetraminoCubeInfo sortedItem = itens[sortedIndex];

                while (sortedItem.TetraminoCubeType == TetraminoCubeType.NORMAL)
                {
                    sortedIndex = UnityEngine.Random.Range(0, itens.Count);
                    sortedItem = itens[sortedIndex];
                }

                return sortedItem;
            }

            return null;
        }

        private TetraminoCubeInfo SetNormalCube()
        {
            IList<TetraminoCubeInfo> itens = GetCubeInfo();
            if (itens != null)
            {
                for (int i = 0; i < itens.Count; i++)
                {
                    if (itens[i].TetraminoCubeType == TetraminoCubeType.NORMAL)
                    {
                        return itens[i];
                    }
                }
            }

            return null;
        }

        private IList<TetraminoCubeInfo> GetCubeInfo()
        {
            AsyncOperationHandle<IList<TetraminoCubeInfo>> cubeTypesLoadOpHandle = m_tetraminoSpawner.CubeTypesLoadOpHandle;
            if (cubeTypesLoadOpHandle.Status == AsyncOperationStatus.Succeeded)
            {
                return cubeTypesLoadOpHandle.Result;
            }
            return null;
        }

        private IEnumerator TetraminoFall()
        {
            if (!m_canMove) yield break;
            yield return new WaitForSeconds(m_currentSpeed);

            transform.position += Vector3.down;
            // When the grid position is invalid the tetramino returns to the viewing position.
            if (m_tetraminoManager.ThisPositionIsValid(m_tetraminoCubes) == false)
            {
                Move(Vector3.up, ParentAnchor.transform);
                TetraminoBeat();
            }

            StartCoroutine(TetraminoFall());
        }

        // Move tetramino for the specified position.
        private void Move(Vector3 direction, Transform objectToMove)
        {
            objectToMove.position += direction;
        }

        // when tetamino arrives at another tetromin or at the lower limit of the grid this method is called.
        private void TetraminoBeat()
        {
            m_canMove = false;
            m_signalBus.Fire(new OnTetratiminoFinishMovementSignal(m_tetraminoCubes));
            DeleteParent();
        }

        // When tetramino is instantiated, this function changes the speed based on the score.
        private void SetVelocity()
        {
            if (m_iScore.GetCurrentPoints() > 700)
            {
                m_normalSpeed /= 3;
            }
            else if (m_iScore.GetCurrentPoints() >= 1200)
            {
                m_normalSpeed /= 2;
            }
            else if (m_iScore.GetCurrentPoints() >= 1700)
            {
                m_normalSpeed /= 1f;
            }
        }

        private void DeleteParent()
        {
            Transform anchor;
            (anchor = transform).DetachChildren();
            Destroy(anchor.parent.gameObject);
        }

        private void InputVerifier()
        {
            if (!UnityEngine.Input.anyKeyDown && !UnityEngine.Input.GetKeyUp(KeyCode.DownArrow)) return;

            switch (m_iInputType.VerifyInput())
            {
                case MovementType.ROTATE:
                    Rotate();
                    break;
                case MovementType.MOVE_RIGHT:
                    MoveRight();
                    break;
                case MovementType.MOVE_LEFT:
                    MoveLeft();
                    break;
                case MovementType.MOVE_FAST:
                    m_currentSpeed = m_fastSpeed;
                    break;
                case MovementType.NORMAL_MOVEMENT:
                    NormalMovement();
                    break;
                case MovementType.SKIP:
                    Skip();
                    break;
            }
        }

        private void SetIDs(Guid guid)
        {
            for (int i = 0; i < m_tetraminoCubes.Length; i++)
            {
                m_tetraminoCubes[i].SetId(guid);
            }
        }

        #region Tetramino Movimentation Methods

        private void Rotate()
        {
            transform.Rotate(0, 0, -90);
            if (m_tetraminoManager.ThisPositionIsValid(m_tetraminoCubes) == false)
            {
                transform.Rotate(0, 0, 90);
            }
        }

        private void MoveRight()
        {
            Move(Vector3.right, ParentAnchor.transform);
            if (m_tetraminoManager.ThisPositionIsValid(m_tetraminoCubes) == false)
            {
                Move(-Vector3.right, ParentAnchor.transform);
            }
        }

        private void MoveLeft()
        {
            Move(Vector3.left, ParentAnchor.transform);
            if (m_tetraminoManager.ThisPositionIsValid(m_tetraminoCubes) == false)
            {
                Move(-Vector3.left, ParentAnchor.transform);
            }
        }

        private void Skip()
        {
            m_currentSpeed = 0;
            m_soundController.ChangeSfx(0);
            m_soundController.PlaySfx();
        }

        private void NormalMovement() => m_currentSpeed = m_normalSpeed;

        #endregion
    }
}