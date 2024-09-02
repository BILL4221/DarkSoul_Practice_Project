using System;
using APAtelier.DS.UI;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public class ActorStat
    {
        private float hp;
        private float stamina;
        private float maxHp;
        private float maxStamina;

        private Action<float> OnHPChanged;
        private Action<float> OnStaminaChanged;

        public float Stamina => stamina;

        public ActorStat(float maxHp, float maxStamina, PlayerUI playerUI)
        {
            this.maxHp = maxHp;
            this.maxStamina = maxStamina;
            hp = maxHp;
            stamina = maxStamina;
            OnHPChanged = playerUI.UpdateHP;
            OnStaminaChanged = playerUI.UpdateStamina;
        }

        public void AddHp(float value)
        {
            hp = Mathf.Clamp(hp + value, 0, maxHp);
            OnHPChanged?.Invoke(hp/maxHp);
        }
        
        public void AddStamina(float value)
        {
            stamina = Mathf.Clamp(stamina + value, 0, maxStamina);
            OnStaminaChanged?.Invoke(stamina/maxStamina);
        }
    }
}