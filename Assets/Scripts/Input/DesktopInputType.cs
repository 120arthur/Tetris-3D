using UnityEngine;

namespace Input
{
    public class DesktopInputType : IInputType
    {
        public InputMovement VerifyInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
            {
                return InputMovement.Rotate;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
            {
                return InputMovement.MoveLeft;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
            {
                return InputMovement.MoveRight;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow))
            {
                return InputMovement.MoveFast;
            }
            else if (UnityEngine.Input.GetKeyUp(KeyCode.DownArrow))
            {
                return InputMovement.NormalMovement;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                return InputMovement.Skip;
            }
            else
            {
                return InputMovement.Empty;
            }
        }
    }
}
