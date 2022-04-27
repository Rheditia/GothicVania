using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerInput input;
    InputAction moveAction;
    InputAction jumpAction;

    public Vector2 MoveInput { get; private set; }

    public bool JumpInput => jumpBufferTimer > 0;
    [SerializeField] float jumpBufferDuration = 0.2f;
    private float jumpBufferTimer = 0f;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        moveAction = input.actions["Move"];
        jumpAction = input.actions["Jump"];
    }

    private void OnEnable()
    {
        moveAction.started += OnMoveInput;
        moveAction.performed += OnMoveInput;
        moveAction.canceled += OnMoveInput;

        jumpAction.started += OnJumpInput;
        jumpAction.canceled += OnJumpInput;
    }

    private void OnDisable()
    {
        moveAction.started -= OnMoveInput;
        moveAction.performed -= OnMoveInput;
        moveAction.canceled -= OnMoveInput;

        jumpAction.started -= OnJumpInput;
        jumpAction.canceled -= OnJumpInput;
    }

    private void Update()
    {
        JumpBufferCountdown();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        //Debug.Log(MoveInput);
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.ReadValue<float>() == 1) { jumpBufferTimer = jumpBufferDuration; }
    }

    private void JumpBufferCountdown()
    {
        if (jumpBufferTimer > 0) { jumpBufferTimer -= Time.deltaTime; }
        else { return; }
    }

    public void ClearJumpBuffer() => jumpBufferTimer = 0f;
}
