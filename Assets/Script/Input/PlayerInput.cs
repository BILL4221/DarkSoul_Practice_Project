using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APAtelier.DS.Input
{
    public struct PlayerInput
    {
        public HashSet<InputKey> PressKey;
        public Dictionary<AxisKey, float> PressAxisKey;
    }
}