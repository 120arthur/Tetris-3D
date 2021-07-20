using System;
using System.Collections;
using Context;
using Tetrismechanic;
using UnityEngine;

public class TetraminoMovment : MonoBehaviour
{
    [SerializeField] private float _fastSpeed;
    [SerializeField] private float _normalSpeed;
    private float _currentSpeed;

    private bool _canMove = true;

    private GameObject _parentAnchor;

    private TetraminoManager _tetraminoManager;

    private void Start()
    {
        _parentAnchor = transform.parent.gameObject;
        _tetraminoManager = ContextProvider.Context.TetraminoManager;
        SetVelocity();
        _currentSpeed = _normalSpeed;
        StartCoroutine(TetraminoFall());
    }

    private void Update()
    {
        InputVerifyer();
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
    private void Move(Vector3 direction, Transform objectToMove)
    {
        objectToMove.position += direction;
    }

    // when tetamino arrives at another tetromin or at the lower limit of the grid this method is called.
    public void TetraminoBeat()
    {
        _canMove = false;
        Move(Vector3.up, transform);
        _tetraminoManager.FinishTetraminoMovment(gameObject.transform);
        _tetraminoManager.SearchForFullLines();
        DeletePaent();
    }

    // When tetramino is instantiated, this function changes the speed based on the score.
    private void SetVelocity()
    {
        if (ContextProvider.Context.Score.CurrentPoints() > 700)
        {
            _normalSpeed /= 3;
        }
        else if (ContextProvider.Context.Score.CurrentPoints() >= 1200)
        {
            _normalSpeed /= 2;
        }
        else if (ContextProvider.Context.Score.CurrentPoints() >= 1700)
        {
            _normalSpeed /= 1f;
        }
    }

    public void DeletePaent()
    {
        transform.DetachChildren();
        Destroy(transform.parent.gameObject);
    }

    void InputVerifyer()
    {
        if (Input.anyKeyDown || Input.GetKeyUp(KeyCode.DownArrow))
        {
            switch (ContextProvider.Context.InputType.VerifyInput())
            {
                case InputMovment.Rotate:
                    Rotate();
                    break;

                case InputMovment.MoveRight:
                    MoveRight();
                    break;

                case InputMovment.MoveLeft:
                    MoveLeft();
                    break;

                case InputMovment.MoveFast:
                    _currentSpeed = _fastSpeed;
                    break;

                case InputMovment.NormalMovment:
                    NormalMovment();

                    break;

                case InputMovment.Skip:
                    skip();
                    break;

                case InputMovment.Empty:
                    break;

            }
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
    private void skip()
    {
        _currentSpeed = 0;
        SoundController.SoundControllerInstance.ChangeSfx(0);
        SoundController.SoundControllerInstance.PlaySfx();
    }
    private void NormalMovment()
    {
        _currentSpeed = _normalSpeed;
    }
    #endregion
}
