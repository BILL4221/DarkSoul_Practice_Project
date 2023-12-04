using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APAtelier.DS.Input
{
    public enum InputStyle
    {
        Keyboard,
        JoyStick,
    }

    public abstract class InputHandler
    {
        protected InputStyle inputStyle;

        public abstract PlayerInput GetInput();

        public InputStyle GetInputStyle()
        {
            return inputStyle;
        }
    }
}