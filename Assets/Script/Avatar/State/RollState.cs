using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public class RollState : BasePlayerState
    {
        private const string resetSpeedFlag = "ResetSpeed";
        private bool resetSpeed;
        
        public RollState(Player player) : base(player)
        {
            //Do nothing;
            resetSpeed = false;
            player.ActionFlag = ActionFlag.None;
        }
        
        public override void OnStateStart()
        {
            _animancerComponent.Stop();
            var state = _animancerComponent.Play(_clips[AnimationEnum.Roll]);
            state.Events.OnEnd += BackToIdleState;
            state.Events.SetCallback(resetSpeedFlag, ResetSpeed);
        }

        private void ResetSpeed()
        {
            resetSpeed = true;
            player.ActionFlag = ActionFlag.Walkable | ActionFlag.Rollable | ActionFlag.LAttack;
        }

        public override void OnStateUpdate()
        {
            if (!resetSpeed)
            {
                var speed = 10.0f;
                player.transform.position += Time.deltaTime * speed * player.transform.forward;
            }
        }
        
        private void BackToIdleState()
        {
            player.SetState(new IdleState(player));
        }
    }
}