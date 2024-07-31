using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CombatController
{
    [SerializeField] private float speed;
    [SerializeField] private float speedRotation;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private Animator anim;
    private Vector3 _velocity;
    private bool groundedPlayer;
    private Vector3 playerVelocity;

    private void OnEnable()
    {
        GlobalEventManager.OnSendMoveInput += OnGetInput;
    }

    private void OnGetInput(Vector3 vector)
    {
        _velocity = vector.normalized * speed * Time.deltaTime;
    }

    public override void ExcuteMove(Vector3 pos)
    {
        base.ExcuteMove(pos);
        if (pos != Vector3.zero)
        {
            _controller.Move(pos);
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (groundedPlayer == false)
        {
            playerVelocity.y = gravityValue * Time.deltaTime;
            _controller.Move(playerVelocity);
            
        }
    }

    public override void ExecuteRotate(Vector3 eurAngle)
    {
        base.ExecuteRotate(eurAngle);
        if (eurAngle != Vector3.zero)
        {
            Vector3 root = Vector3.Lerp(transform.forward, eurAngle, speedRotation);
            transform.forward = root;
        }
        
    }

    private void FixedUpdate()
    {
        ExcuteMove(_velocity);
        ExecuteRotate(_velocity);
    }

    private void OnDisable()
    {
        GlobalEventManager.OnSendMoveInput -= OnGetInput;
    }
}
