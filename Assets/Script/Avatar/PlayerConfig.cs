using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig", order = 1)]
    public class PlayerConfig : ScriptableObject
    {
        public float BaseHP;
        public float BaseStamina;
        public float RunRate;
        public float StaminaRegenRate;
        public float RollCost;
    }
}