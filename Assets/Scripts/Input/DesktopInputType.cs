using UnityEngine;

namespace Input
{
    public class DesktopInputType : IInputType
    {
        public MovementType VerifyInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
            {
                return MovementType.ROTATE;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
            {
                return MovementType.MOVE_LEFT;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
            {
                return MovementType.MOVE_RIGHT;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow))
            {
                return MovementType.MOVE_FAST;
            }
            else if (UnityEngine.Input.GetKeyUp(KeyCode.DownArrow))
            {
                return MovementType.NORMAL_MOVEMENT;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                return MovementType.SKIP;
            }
            else
            {
                return MovementType.EMPTY;
            }
        }
    }
}
