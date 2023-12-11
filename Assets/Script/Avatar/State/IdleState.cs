using Animancer;
using APAtelier.DS.Input;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public class IdleState : BasePlayerState
    {
        public IdleState(Player player) : base(player)
        {
            //Do nothing
        }
        
        public override void OnStateStart()
        {
            _animancerComponent.Play(_clips[AnimationEnum.Idle]);
        }

        public override void OnStateUpdate()
        {
            var input = _controller.GetInput();

            var moveValue = Vector3.zero;
            if (input.PressAxisKey.TryGetValue(AxisKey.MoveHorizontal, out var hValue))
            {
                moveValue.x += hValue;
            }
            
            if (input.PressAxisKey.TryGetValue(AxisKey.MoveVertical, out var vValue))
            {
                moveValue.z += vValue;
            }

            if (input.PressKey.Contains(InputKey.Roll))
            {
                player.SetState(new RollState(player));
            }
            
            var speed = 5.0f;
            // Calculate the movement direction based on camera rotation
            Vector3 movementDirection = Quaternion.Euler(0, _playerCamera.transform.eulerAngles.y, 0) * new Vector3(moveValue.x, 0, moveValue.z);
            player.transform.position += Time.deltaTime * speed * movementDirection;
            
            ((LinearMixerTransitionAsset)_clips[AnimationEnum.Idle]).Transition.State.Parameter = moveValue.magnitude;
            
            // Rotate the player to face the movement direction
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation, 60 * Time.deltaTime * 100);
            }
        }
    }
}
