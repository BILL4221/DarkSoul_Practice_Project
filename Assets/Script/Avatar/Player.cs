using System;
using Animancer;
using APAtelier.DS.Input;
using APAtelier.DS.UI;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public enum AnimationEnum
    {
        Idle,
        Roll,
    }
    
    public enum AttackStateEnum
    {
        CastSpell,
    }
    
    [Flags]
    public enum ActionFlag
    {
        None,
        Walkable,
        Rollable,
        LAttack,
    }

    public class Player : Actor
    {
        [SerializeField] 
        private PlayerConfig config;
        [SerializeField] 
        private LinearMixerTransitionAsset idleClip;
        [SerializeField] 
        private SerializableDictionaryBase<AnimationEnum, AnimancerTransitionAssetBase> _clips;
        [SerializeField] 
        private SerializableDictionaryBase<AttackStateEnum, AttackStateParameter> _attackStateClips;
        [SerializeField] 
        private Camera _playerCamera;
        [SerializeField] 
        private PlayerUI _playerUI;
        private PlayerController _controller;
        private float idleTimer;
        private float cameraStartDistance;
        private Vector3 rotateValue;
        private BasePlayerState currentState;
        
        public PlayerController Controller => _controller;
        public AnimancerComponent AnimancerComponent => _animancerComponent;
        public SerializableDictionaryBase<AnimationEnum, AnimancerTransitionAssetBase> Clips => _clips;
        public SerializableDictionaryBase<AttackStateEnum, AttackStateParameter> AttackStateClips => _attackStateClips;
        public Camera PlayerCamera => _playerCamera;
        [HideInInspector] 
        public ActionFlag ActionFlag;
        public PlayerConfig Config => config;
        
        private void Awake()
        {
            _controller = new PlayerController();
            _controller.RegisterInputController(new JoyStickController());
            idleTimer = 0f;
            _animancerComponent.Play(idleClip);
            cameraStartDistance = 3.2f;
            rotateValue = Vector3.zero;
            SetState(new IdleState(this));
            _actorStat = new ActorStat(config.BaseHP, config.BaseStamina, _playerUI);
        }

        private void Update()
        {
            currentState?.OnStateUpdate();
            UpdateCamera();
            CheckInterruptAction();
        }

        private void CheckInterruptAction()
        {
            var input = _controller.GetInput();
            
            if (ActionFlag.HasFlag(ActionFlag.Rollable) && input.PressKey.Contains(InputKey.Roll) && _actorStat.Stamina >= config.RollCost)
            {
                _actorStat.AddStamina(-config.RollCost);
                SetState(new RollState(this));
            }
            
            var moveValue = Vector3.zero;
            if (input.PressAxisKey.TryGetValue(AxisKey.MoveHorizontal, out var hValue))
            {
                moveValue.x += hValue;
            }
            
            if (input.PressAxisKey.TryGetValue(AxisKey.MoveVertical, out var vValue))
            {
                moveValue.z += vValue;
            }
            
            if (ActionFlag.HasFlag(ActionFlag.Walkable) && moveValue != Vector3.zero)
            {
                SetState(new IdleState(this));
            }
            
            if (ActionFlag.HasFlag(ActionFlag.LAttack) && input.PressKey.Contains(InputKey.LAttack) && _actorStat.Stamina >= _attackStateClips[AttackStateEnum.CastSpell].StatminaCost)
            {
                _actorStat.AddStamina(-_attackStateClips[AttackStateEnum.CastSpell].StatminaCost);
                SetState(new AttackState(this, _attackStateClips[AttackStateEnum.CastSpell]));
            }
        }

        private void UpdateCamera()
        {
            // Get right analog stick input
            float horizontalInput = UnityEngine.Input.GetAxis("RightHorizontal");
            float verticalInput = UnityEngine.Input.GetAxis("RightVertical");

            // Calculate the rotation based on input
            rotateValue += new Vector3(horizontalInput, verticalInput, 0) * 120f * Time.deltaTime;

            // Calculate the camera position relative to the player
            Vector3 offset = Quaternion.Euler(rotateValue) * new Vector3(0, 0, -cameraStartDistance);
            Vector3 newPosition = transform.position + Vector3.up * 0.7f + offset;

            // Set the camera position
            _playerCamera.transform.position = newPosition;

            // Make sure the camera is always looking at the player
            _playerCamera.transform.LookAt(transform.position + Vector3.up * 0.7f);
        }


        public void SetState(BasePlayerState state)
        {
            currentState?.OnStateEnd();
            currentState = state;
            currentState.OnStateStart();
        }
    }
}
