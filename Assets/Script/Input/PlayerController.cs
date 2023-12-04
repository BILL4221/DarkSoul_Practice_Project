using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace APAtelier.DS.Input
{
    public struct InputController
    {
        public bool Enable;
        public InputHandler Handler;

        public InputController(bool enable, InputHandler handler)
        {
            Enable = enable;
            Handler = handler;
        }
    }

    public class PlayerController
    {
        private Dictionary<InputStyle, InputController> _inputs;
        private InputStyle lastAvaliableInputStyle;
        
        public PlayerController()
        {
            _inputs = new Dictionary<InputStyle, InputController>();
        }

        public void RegisterInputController(InputHandler handler)
        {
            var inputController = new InputController(true, handler);
            _inputs[handler.GetInputStyle()] = inputController;
        }

        public PlayerInput GetInput()
        {
            var allInput = new PlayerInput();
            allInput.PressKey = new HashSet<InputKey>();
            allInput.PressAxisKey = new Dictionary<AxisKey, float>();
            
            foreach (var inputController in _inputs)
            {
                if (inputController.Value.Enable)
                {
                    var input = inputController.Value.Handler.GetInput();
                    allInput.PressKey.AddRange(input.PressKey);
                    allInput.PressAxisKey.AddRange(input.PressAxisKey);
                }
            }

            return allInput;
        }
        
        public void EnableInputStyle(InputStyle style, bool enable)
        {
            if (_inputs.ContainsKey(style))
            {
                var inputController = _inputs[style];
                inputController.Enable = enable;
            }
            
            Debug.LogWarning("No Input Style: " + style + "in this controller");
        }

        public bool GetEnableInputStyle(InputStyle style)
        {
            if (_inputs.ContainsKey(style))
            {
                return _inputs[style].Enable;
            }

            Debug.LogWarning("No Input Style: " + style + "in this controller");
            return false;
        }
    }
}