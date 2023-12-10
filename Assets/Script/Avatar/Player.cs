using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using APAtelier.DS.Input;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public class Player : Actor
    {
        [SerializeField] 
        private LinearMixerTransitionAsset idleClip;
        [SerializeField] 
        private ClipTransitionAsset rollClip;
        [SerializeField] 
        private Camera playerCamera;
        private PlayerController _controller;
        private float idleTimer;
        private float cameraStartDistance;
        private Vector3 rotateValue;
        
        private void Awake()
        {
            _controller = new PlayerController();
            _controller.RegisterInputController(new JoyStickController());
            idleTimer = 0f;
            _animancerComponent.Play(idleClip);
            cameraStartDistance = 3.2f;
            rotateValue = Vector3.zero;
        }

        private void Update()
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
                _animancerComponent.Play(rollClip).Events.OnEnd += Idle;
            }
            
            var speed = 5.0f;
            // Calculate the movement direction based on camera rotation
            Vector3 movementDirection = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0) * new Vector3(moveValue.x, 0, moveValue.z);
            transform.position += Time.deltaTime * speed * movementDirection;
            
            idleClip.Transition.State.Parameter = moveValue.magnitude;
            
            // Rotate the player to face the movement direction
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 60 * Time.deltaTime * 100);
            }

            // Get right analog stick input
            float horizontalInput = UnityEngine.Input.GetAxis("RightHorizontal");
            float verticalInput = UnityEngine.Input.GetAxis("RightVertical");

            // Calculate the rotation based on input
            rotateValue += new Vector3(horizontalInput, verticalInput, 0) * 120f * Time.deltaTime;

            // Calculate the camera position relative to the player
            Vector3 offset = Quaternion.Euler(rotateValue) * new Vector3(0, 0, -cameraStartDistance);
            Vector3 newPosition = transform.position + Vector3.up * 0.7f + offset;

            // Set the camera position
            playerCamera.transform.position = newPosition;

            // Make sure the camera is always looking at the player
            playerCamera.transform.LookAt(transform.position + Vector3.up * 0.7f);

        }

        private void Idle()
        {
            _animancerComponent.Play(idleClip);
        }
    }
}
