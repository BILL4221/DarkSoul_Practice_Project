using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public class RollState : BasePlayerState
    {
        public RollState(Player player) : base(player)
        {
            //Do nothing;
        }
        
        public override void OnStateStart()
        {
            _animancerComponent.Play(_clips[AnimationEnum.Roll]).Events.OnEnd += BackToIdleState;
        }

        public override void OnStateUpdate()
        {
            var speed = 10.0f;
            player.transform.position += Time.deltaTime * speed * player.transform.forward;
        }
        
        private void BackToIdleState()
        {
            player.SetState(new IdleState(player));
        }
    }
}