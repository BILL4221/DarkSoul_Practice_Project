using Animancer;
using APAtelier.DS.Input;
using RotaryHeart.Lib.SerializableDictionary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace APAtelier.DS.Avatar
{
    public enum AnimationEnum
    {
        Idle,
        Roll,
    }
    
    public class Player : Actor
    {
        [SerializeField] 
        private LinearMixerTransitionAsset idleClip;
        [SerializeField] 
        private ClipTransitionAsset rollClip;
        [SerializeField] 
        private SerializableDictionaryBase<AnimationEnum, AnimancerTransitionAssetBase> _clips;
        [SerializeField] 
        private Camera _playerCamera;
        private PlayerController _controller;
        private float idleTimer;
        private float cameraStartDistance;
        private Vector3 rotateValue;
        private BasePlayerState currentState;
        
        public PlayerController Controller => _controller;
        public AnimancerComponent AnimancerComponent => _animancerComponent;
        public SerializableDictionaryBase<AnimationEnum, AnimancerTransitionAssetBase> Clips => _clips;
        public Camera PlayerCamera => _playerCamera;
        
        private void Awake()
        {
            _controller = new PlayerController();
            _controller.RegisterInputController(new JoyStickController());
            idleTimer = 0f;
            _animancerComponent.Play(idleClip);
            cameraStartDistance = 3.2f;
            rotateValue = Vector3.zero;
            SetState(new IdleState(this));
        }

        private void Update()
        {
            currentState?.OnStateUpdate();
            
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

        private void Idle()
        {
            _animancerComponent.Play(idleClip);
        }

        public void SetState(BasePlayerState state)
        {
            currentState?.OnStateEnd();
            currentState = state;
            currentState.OnStateStart();
        }
    }
}
