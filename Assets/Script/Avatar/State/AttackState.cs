using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public class AttackState : BasePlayerState
    {
        private AttackStateParameter _attackStateParameter;
        
        public AttackState(Player player, AttackStateParameter attackStateParameter) : base(player)
        {
            _attackStateParameter = attackStateParameter;
            _attackStateParameter.SetPlayer(player);
            player.ActionFlag = ActionFlag.None;
        }
        
        public override void OnStateStart()
        {
            _animancerComponent.Stop();
            var state = _animancerComponent.Play(_attackStateParameter.Transition);
            state.Events.OnEnd += BackToIdleState;
        }
        
        private void BackToIdleState()
        {
            player.SetState(new IdleState(player));
        }
    }
}
