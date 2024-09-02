using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APAtelier.DS.Input
{
    public class JoyStickController : InputHandler
    {
        public JoyStickController()
        {
            inputStyle = InputStyle.JoyStick;
        }

        public override PlayerInput GetInput()
        {
            var input = new PlayerInput();
            input.PressKey = new HashSet<InputKey>();
            input.PressAxisKey = new Dictionary<AxisKey, float>();
            
            var axisHorizontal = UnityEngine.Input.GetAxis("Horizontal");
            input.PressAxisKey[AxisKey.MoveHorizontal] = axisHorizontal;
            var axisVertical = UnityEngine.Input.GetAxis("Vertical");
            input.PressAxisKey[AxisKey.MoveVertical] = axisVertical;

            if (UnityEngine.Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                input.PressKey.Add(InputKey.Roll);
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                input.PressKey.Add(InputKey.LAttack);
            }
            
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Joystick1Button9))
            {
                input.PressKey.Add(InputKey.LockTarget);
            }
            return input;
        }
    }
}
