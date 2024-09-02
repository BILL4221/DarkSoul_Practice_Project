using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace APAtelier.DS.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] 
        private Image hpGauge;
        [SerializeField] 
        private Image staminaGauge;

        public void UpdateHP(float value)
        {
            hpGauge.fillAmount = value;
        }
        
        public void UpdateStamina(float value)
        {
            staminaGauge.fillAmount = value;
        }
    }
}