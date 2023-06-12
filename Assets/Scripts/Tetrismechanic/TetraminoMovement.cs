using System.Collections;
//using Context;
using Input;
using Score;
using Sound;
using UnityEngine;
using Zenject;

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
        private DesktopInputType m_DesktopInputType;

        [SerializeField] private float fastSpeed;
        [SerializeField] private float normalSpeed;
        private float _currentSpeed;

        private bool _canMove = true;

        private GameObject _parentAnchor;

        private void Start()
        {
            _parentAnchor = transform.parent.gameObject;
            SetVelocity();
            _currentSpeed = normalSpeed;
            StartCoroutine(TetraminoFall());
        }

        private void Update()
        {
            InputVerifier();
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

            if (m_DesktopInputType.VerifyInput() == InputMovement.Rotate)
            {
                Rotate();
            }
            else if (m_DesktopInputType.VerifyInput() == InputMovement.MoveRight)
            {
                MoveRight();
            }
            else if (m_DesktopInputType.VerifyInput() == InputMovement.MoveLeft)
            {
                MoveLeft();
            }
            else if (m_DesktopInputType.VerifyInput() == InputMovement.MoveFast)
            {
                _currentSpeed = fastSpeed;
            }
            else if (m_DesktopInputType.VerifyInput() == InputMovement.NormalMovement)
            {
                NormalMovement();
            }
            else if (m_DesktopInputType.VerifyInput() == InputMovement.Skip)
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