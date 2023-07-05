using System.Collections;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Input;
using Score;
using Sound;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;

namespace TetrisMechanic
{
    public class TetraminoMovement : MonoBehaviour
    {
        [Inject]
        private SoundController m_SoundController;
        [Inject]
        private TetraminoManager m_TetraminoManager;
        [Inject]
        private ScoreManager m_ScoreManager;
        [Inject]
        private IInputType m_IInputType;

        private AsyncOperationHandle<IList<TetraminoCubeInfo>> m_CubeTypesLoadOpHandle;
        private List<string> m_Keys = new List<string>() { "CubeTypes" };

        [SerializeField] private float fastSpeed;
        [SerializeField] private float normalSpeed;
        [SerializeField] private TetraminoCube[] m_TetraminoCubes;

        private float _currentSpeed;

        private bool _canMove = true;

        private GameObject _parentAnchor;

        private void Start()
        {
            _parentAnchor = transform.parent.gameObject;
            SetVelocity();
            _currentSpeed = normalSpeed;
            StartCoroutine(TetraminoFall());

            m_CubeTypesLoadOpHandle = Addressables.LoadAssetsAsync<TetraminoCubeInfo>(m_Keys, null, Addressables.MergeMode.Union);
            m_CubeTypesLoadOpHandle.Completed += OnCubeTypesLadComplete;

        }

        private void OnCubeTypesLadComplete(AsyncOperationHandle<IList<TetraminoCubeInfo>> asyncOperationHandle)
        {
            Debug.Log("AsyncOperationHandle Status: " + asyncOperationHandle.Status);

            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                SetTetraminoCubes();
            }
        }

        private void Update()
        {
            InputVerifier();
        }

        private void SetTetraminoCubes()
        {
            float random = Random.Range(1f, 100f);
            if (random < 80)
            {
                m_TetraminoCubes[Random.Range(1, m_TetraminoCubes.Length)].SetTetraminoCubeInfo(SortSpecialCube());
            }

            for (int i = 0; i < m_TetraminoCubes.Length; i++)
            {
                if (m_TetraminoCubes[i].TetraminoCubeType == TetraminoCubeType.NORMAL)
                {
                    m_TetraminoCubes[i].SetTetraminoCubeInfo(SetNormalCube());
                }
            }
        }

        private TetraminoCubeInfo SortSpecialCube()
        {
            var itens = m_CubeTypesLoadOpHandle.Result;

            int sortedIndex = Random.Range(0, itens.Count);
            TetraminoCubeInfo sortedItem = itens[sortedIndex];

            while (sortedItem.TetraminoCubeType == TetraminoCubeType.NORMAL)
            {
                sortedIndex = Random.Range(0, itens.Count);
                sortedItem = itens[sortedIndex];
            }

            return sortedItem;
        }

        private TetraminoCubeInfo SetNormalCube()
        {
            var itens = m_CubeTypesLoadOpHandle.Result;

            for (int i = 0; i < itens.Count; i++)
            {
                if (itens[i].TetraminoCubeType == TetraminoCubeType.NORMAL)
                {
                    return itens[i];
                }
            }

            return null;
        }

        private IEnumerator TetraminoFall()
        {
            if (!_canMove) yield break;
            yield return new WaitForSeconds(_currentSpeed);

            transform.position += Vector3.down;
            // When the grid position is invalid the tetramino returns to the viewing position.
            if (m_TetraminoManager.ThisPositionIsValid(gameObject.transform) == false)
            {
                TetraminoBeat();
            }

            StartCoroutine(TetraminoFall());
        }

        // Move tetramino for the specified position.
        private void Move(Vector3 direction, Transform objectToMove) => objectToMove.position += direction;

        // when tetamino arrives at another tetromin or at the lower limit of the grid this method is called.
        private void TetraminoBeat()
        {
            _canMove = false;
            Move(Vector3.up, transform);
            m_TetraminoManager.FinishTetraminoMovement(gameObject.transform);
            m_TetraminoManager.SearchForFullLines();
            DeleteParent();
        }

        // When tetramino is instantiated, this function changes the speed based on the score.
        private void SetVelocity()
        {
            if (m_ScoreManager.CurrentPoints() > 700)
            {
                normalSpeed /= 3;
            }
            else if (m_ScoreManager.CurrentPoints() >= 1200)
            {
                normalSpeed /= 2;
            }
            else if (m_ScoreManager.CurrentPoints() >= 1700)
            {
                normalSpeed /= 1f;
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

            if (m_IInputType.VerifyInput() == InputMovement.Rotate)
            {
                Rotate();
            }
            else if (m_IInputType.VerifyInput() == InputMovement.MoveRight)
            {
                MoveRight();
            }
            else if (m_IInputType.VerifyInput() == InputMovement.MoveLeft)
            {
                MoveLeft();
            }
            else if (m_IInputType.VerifyInput() == InputMovement.MoveFast)
            {
                _currentSpeed = fastSpeed;
            }
            else if (m_IInputType.VerifyInput() == InputMovement.NormalMovement)
            {
                NormalMovement();
            }
            else if (m_IInputType.VerifyInput() == InputMovement.Skip)
            {
                Skip();
            }
        }

        #region Tetramino Movimentation Methods

        private void Rotate()
        {
            transform.Rotate(0, 0, -90);
            if (m_TetraminoManager.ThisPositionIsValid(gameObject.transform) == false)
            {
                transform.Rotate(0, 0, 90);
            }
        }

        private void MoveRight()
        {
            Move(Vector3.right, _parentAnchor.transform);
            if (m_TetraminoManager.ThisPositionIsValid(gameObject.transform) == false)
            {
                Move(-Vector3.right, _parentAnchor.transform);
            }
        }

        private void MoveLeft()
        {
            Move(Vector3.left, _parentAnchor.transform);
            if (m_TetraminoManager.ThisPositionIsValid(gameObject.transform) == false)
            {
                Move(-Vector3.left, _parentAnchor.transform);
            }
        }

        private void Skip()
        {
            _currentSpeed = 0;
            m_SoundController.ChangeSfx(0);
            m_SoundController.PlaySfx();
        }

        private void NormalMovement() => _currentSpeed = normalSpeed;

        #endregion
    }
}