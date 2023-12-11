using Animancer;
using APAtelier.DS.Input;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public abstract class BasePlayerState : IStateble
    {
        protected Player player;
        protected PlayerController _controller;
        protected AnimancerComponent _animancerComponent;
        protected SerializableDictionaryBase<AnimationEnum, AnimancerTransitionAssetBase> _clips;
        protected Camera _playerCamera;
        
        protected BasePlayerState(Player player)
        {
            this.player = player;
            _controller = player.Controller;
            _animancerComponent = player.AnimancerComponent;
            _clips = player.Clips;
            _playerCamera = player.PlayerCamera;
        }

        public virtual void OnStateStart()
        {
            
        }

        public virtual void OnStateUpdate()
        {
            
        }

        public virtual void OnStateEnd()
        {

        }
    }
}
