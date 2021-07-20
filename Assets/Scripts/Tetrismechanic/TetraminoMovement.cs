using System.Collections;
using Context;
using Input;
using Sound;
using UnityEngine;

namespace TetrisMechanic
{
    public class TetraminoMovement : MonoBehaviour
    {
        [SerializeField] private float fastSpeed;
        [SerializeField] private float normalSpeed;
        private float _currentSpeed;

        private bool _canMove = true;

        private GameObject _parentAnchor;

        private TetraminoManager _tetraminoManager;

        private void Start()
        {
            _parentAnchor = transform.parent.gameObject;
            _tetraminoManager = ContextProvider.Context.TetraminoManager;
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
            if (_tetraminoManager.ThisPositionIsValid(gameObject.transform) == false)
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
            _tetraminoManager.FinishTetraminoMovement(gameObject.transform);
            _tetraminoManager.SearchForFullLines();
            DeleteParent();
        }

        // When tetramino is instantiated, this function changes the speed based on the score.
        private void SetVelocity()
        {
            if (ContextProvider.Context.Score.CurrentPoints() > 700)
            {
                normalSpeed /= 3;
            }
            else if (ContextProvider.Context.Score.CurrentPoints() >= 1200)
            {
                normalSpeed /= 2;
            }
            else if (ContextProvider.Context.Score.CurrentPoints() >= 1700)
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

            if (ContextProvider.Context.InputType.VerifyInput() == InputMovement.Rotate)
            {
                Rotate();
            }
            else if (ContextProvider.Context.InputType.VerifyInput() == InputMovement.MoveRight)
            {
                MoveRight();
            }
            else if (ContextProvider.Context.InputType.VerifyInput() == InputMovement.MoveLeft)
            {
                MoveLeft();
            }
            else if (ContextProvider.Context.InputType.VerifyInput() == InputMovement.MoveFast)
            {
                _currentSpeed = fastSpeed;
            }
            else if (ContextProvider.Context.InputType.VerifyInput() == InputMovement.NormalMovement)
            {
                NormalMovement();
            }
            else if (ContextProvider.Context.InputType.VerifyInput() == InputMovement.Skip)
            {
                Skip();
            }
        }

        #region Tetramino Movimentation Methods

        private void Rotate()
        {
            transform.Rotate(0, 0, -90);
            if (_tetraminoManager.ThisPositionIsValid(gameObject.transform) == false)
            {
                transform.Rotate(0, 0, 90);
            }
        }

        private void MoveRight()
        {
            Move(Vector3.right, _parentAnchor.transform);
            if (_tetraminoManager.ThisPositionIsValid(gameObject.transform) == false)
            {
                Move(-Vector3.right, _parentAnchor.transform);
            }
        }

        private void MoveLeft()
        {
            Move(Vector3.left, _parentAnchor.transform);
            if (_tetraminoManager.ThisPositionIsValid(gameObject.transform) == false)
            {
                Move(-Vector3.left, _parentAnchor.transform);
            }
        }

        private void Skip()
        {
            _currentSpeed = 0;
            SoundController.SoundControllerInstance.ChangeSfx(0);
            SoundController.SoundControllerInstance.PlaySfx();
        }

        private void NormalMovement() => _currentSpeed = normalSpeed;

        #endregion
    }
}