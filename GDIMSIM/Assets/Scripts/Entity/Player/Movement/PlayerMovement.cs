using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    // Contributors: Taylor
    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform bodyTrans;
    private WallRunning _wallRunning;
    private Dash _dash;
    private PauseMenu _pauseMenu;
    private PlayerTracker _playerTracker;

    [Header("Speed")]
    [SerializeField] private float walkSpeed = 12f;
    [SerializeField] private float sprintSpeed = 20f;
    [SerializeField] private float crouchSpeed = 5f;
    [HideInInspector] public float wallRunSpeed;
    [HideInInspector] public float dashSpeed;
    private float _currentSpeed;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] public LayerMask groundMask;
    private bool _isGrounded;
    
    [Header("Physics")]
    [SerializeField] private float gravity = - 9.81f;
    [HideInInspector] public bool useGravity = true;
    [HideInInspector] public Vector3 velocity;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 3f;
    private bool _canDoubleJump = false;

    [Header("Crouching")]
    [SerializeField] private float crouchYScale;
    private float _startYScale;
    private bool _isCrouching = false;
    private double fallTime = 0.0;
    private bool jumped = false;
    
    // For new input system
    [HideInInspector] public Vector2 movementInput = Vector2.zero;
    private bool _shouldSprint = false;
    private bool _shouldCrouch = false;
    
    // For Spell
    [HideInInspector] public bool spellActive = false;

    // Movement States
    [HideInInspector] public MovementState movementState;

    public enum MovementState
    {
        Walking,
        Sprinting,
        Dashing,
        WallRunning,
        Crouching,
        Air,
        Falling,
    }
    
    // Code has been inspired and modified a bit based on these tutorials
    // https://www.youtube.com/watch?v=f473C43s8nE&t=505s
    // https://www.youtube.com/watch?v=_QajrabyTJc

    // Need this in order for new input system to work with networked multiplayer
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        PlayerInput playerInput = GetComponent<PlayerInput>();
        playerInput.enabled = true;
    }

    public void UpdateSpeed(float change)
    {
        walkSpeed += change;
        sprintSpeed += change;
        crouchSpeed += change;
    }
    
    public bool IsGrounded()
    {
        return _isGrounded;
    }

    public float GetSpeed()
    {
        return _currentSpeed;
    }
    
    private void Start()
    {
        if (!isLocalPlayer)
            return;

        _startYScale = transform.localScale.y;
        _wallRunning = GetComponent<WallRunning>();
        _dash = GetComponent<Dash>();
        _pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        _playerTracker = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<PlayerTracker>();
        _playerTracker.UpdatePlayerList();
    }

    protected void OnDestroy()
    {
        _playerTracker.UpdatePlayerList();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;
        
        // Handles what movement state we are in
        MovementStateHandler();
        
        // Resets falling velocity if they are no longer falling
        ResetVelocity();
        
        // Movement
        MoveInDirection();

        // Force standing if player isn't trying to crouch and is no longer under object
        ForceStandUp();

        // Gravity
        Gravity();
    }

    private void MovementStateHandler()
    {
        // Determines the movement state and speed based on different conditions
        if (_wallRunning.isWallRunning)
        {
            movementState = MovementState.WallRunning;
        }
        else if (_dash.isDashing)
        {
            movementState = MovementState.Dashing;
        }
        else if (_isCrouching)
        {
            movementState = MovementState.Crouching;
            _currentSpeed = crouchSpeed;
        }
        else if (_isGrounded && _shouldSprint)
        {
            movementState = MovementState.Sprinting;
            _currentSpeed = sprintSpeed;
        }
        else if (_isGrounded)
        {
            movementState = MovementState.Walking;
            _currentSpeed = walkSpeed;
        }
        else
        {
            if (fallTime < 0.35 && !jumped)
            {
                movementState = MovementState.Falling;
                fallTime += Time.deltaTime;
            }
            else
                movementState = MovementState.Air;
        }
    }

    private void ResetVelocity()
    {
        // Sphere casts to check for ground
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Makes it so we arent changing velocity when on ground not falling
        if (_isGrounded && velocity.y < 0)
        {
            jumped = false;
            fallTime = 0.0;
            _canDoubleJump = true;
            velocity.y = -2f;
        }
    }

    private void MoveInDirection()
    {
        Transform myTransform = transform;
        Vector3 move = myTransform.right * movementInput.x + myTransform.forward * movementInput.y; // This makes it so its moving locally so rotation is taken into consideration

        controller.Move(move * (_currentSpeed * Time.deltaTime)); // Moving in the direction of move at the speed
    }

    private void DoJump() => velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    
    private bool IsUnderObject()
    {
        float heightAbove = controller.height - crouchYScale; // height length between full stand and crouch
        
        // Ray casts upwards an amount to check if you are under and object. If the raycast hits nothing then you are above ground.
        return Physics.Raycast(transform.position, Vector3.up, heightAbove, groundMask);
    }

    private void ForceStandUp()
    {
        if (_isCrouching && !_shouldCrouch && !IsUnderObject())
        {
            Vector3 localScale = bodyTrans.localScale;
            bodyTrans.localScale = new Vector3(localScale.x, _startYScale, localScale.z);
            _isCrouching = false;
        }
    }

    private void Gravity()
    {
        // If we are currently using gravity this makes us fall
        if (useGravity)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
    
    // New Input system actions below
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        
        if ((_isGrounded || movementState == MovementState.Falling) && movementState != MovementState.Crouching)
        {
            jumped = true;
            DoJump();
        }
        else if ((movementState == MovementState.Air || movementState == MovementState.Falling) && _canDoubleJump && spellActive)
        {
            _canDoubleJump = false;
            DoJump();
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        _shouldSprint = context.action.triggered;
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        Vector3 localScale = bodyTrans.localScale;
        _shouldCrouch = context.action.triggered;
        
        if (context.started && !_isCrouching && movementState != MovementState.WallRunning) // If we push down the crouch key and we are crouching (not wall running) we decrease model size
        {
            bodyTrans.localScale = new Vector3(localScale.x, crouchYScale, localScale.z);
            _isCrouching = true;
        }
        else if (!context.performed && _isCrouching && !IsUnderObject()) // When releasing crouch key sets our scale back to normal
        {
            bodyTrans.localScale = new Vector3(localScale.x, _startYScale, localScale.z);
            _isCrouching = false;
        }
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        
        if (_pauseMenu.IsGamePaused())
        {
            _pauseMenu.ResumeGame();
        }
        else
        {
            _pauseMenu.Pause();
        }
    }
}
