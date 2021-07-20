using System.Collections;
using UnityEngine;

public class DesktopInputType : IInputType
{
    public InputMovment VerifyInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return InputMovment.Rotate;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return InputMovment.MoveLeft;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return InputMovment.MoveRight;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return InputMovment.MoveFast;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            return InputMovment.NormalMovment;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            return InputMovment.Skip;
        }
        else
        {
            return InputMovment.Empty;
        }
    }
}
