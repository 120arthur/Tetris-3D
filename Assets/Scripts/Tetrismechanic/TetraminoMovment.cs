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

    private SoundController _soundControllerInstance;
    private TetraminoManager _tetraminoGridInstance;

    private void Start()
    {
        _parentAnchor = transform.parent.gameObject;
        _tetraminoGridInstance = ContextProvider.Context.TetraminoManager;
        _soundControllerInstance = SoundController.SoundControllerInstance;
        SetVelocity();
        _currentSpeed = _normalSpeed;
        StartCoroutine(TetraminoFall());
    }

    private void Update()
    {
        Inputs();
    }

    private IEnumerator TetraminoFall()
    {
        if (!_canMove) yield break;
        yield return new WaitForSeconds(_currentSpeed);

        transform.position += Vector3.down;
        // When the grid position is invalid the tetramino returns to the viewing position.
        if (_tetraminoGridInstance.ThisPositionIsValid(gameObject.transform) == false)
        {
            TetraminoBeat();
        }

        StartCoroutine(TetraminoFall());
    }

    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);
            if (_tetraminoGridInstance.ThisPositionIsValid(gameObject.transform) == false)
            {
                transform.Rotate(0, 0, 90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left, _parentAnchor.transform);
            if (_tetraminoGridInstance.ThisPositionIsValid(gameObject.transform) == false)
            {
                Move(-Vector3.left, _parentAnchor.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right, _parentAnchor.transform);
            if (_tetraminoGridInstance.ThisPositionIsValid(gameObject.transform) == false)
            {
                Move(-Vector3.right, _parentAnchor.transform);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _currentSpeed = _fastSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _currentSpeed = _normalSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentSpeed = 0;
            _soundControllerInstance.ChangeSfx(0);
            _soundControllerInstance.PlaySfx();
        }
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
        _tetraminoGridInstance.FinishTetraminoMovment(gameObject.transform);
        _tetraminoGridInstance.SearchForFullLines();
        enabled = false;
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
}