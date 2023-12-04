using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APAtelier.DS.Input
{
    public class JoyStickController : InputHandler
    {
        public JoyStickController()
        {
            inputStyle = InputStyle.Keyboard;
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
            
            
            return input;
        }
    }
}
