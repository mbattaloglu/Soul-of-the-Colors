using Game.Player.Abstracts.Animations;
using Game.Player.Abstracts.Inputs;
using Game.Player.Abstracts.Movements;
using Game.Player.Concreates.Animations;
using Game.Player.Concreates.Inputs;
using Game.Player.Concreates.Movement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerInput playerInput;
    private IPlayerMovement playerMovement;
    private IPlayerAnimation playerAnimation;

    [Header("Movement")]
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float jumpForce;

    [Header("Collision Settings")]
    [SerializeField] private float collisionOffset;
    [SerializeField] private ContactFilter2D contactFilter;

    private float initialSpeed;

    private void Awake()
    {
        playerInput = new ComputerInput();
        playerMovement = new MoveWithRigidbody(GetComponent<Rigidbody2D>(), contactFilter, walkingSpeed, jumpForce, collisionOffset);
        playerAnimation = new PlayerAnimation(GetComponent<Animator>());

        //Walk
        ((ComputerInput)playerInput).footActions.Move.performed += ctx => OnWalkStarted();
        ((ComputerInput)playerInput).footActions.Move.canceled += ctx => OnWalkStopped();
        //Run
        ((ComputerInput)playerInput).footActions.Run.performed += ctx => OnRunStarted();
        ((ComputerInput)playerInput).footActions.Run.canceled += ctx => OnRunStopped();
        //Crouch
        ((ComputerInput)playerInput).footActions.Crouch.performed += ctx => OnCrouchStarted();
        ((ComputerInput)playerInput).footActions.Crouch.canceled += ctx => OnCrouchStopped();
        //Jump
        ((ComputerInput)playerInput).footActions.Jump.performed += ctx => OnJumpStarted();
        ((ComputerInput)playerInput).footActions.Jump.canceled += ctx => OnJumpStopped();
    }


    private void Update()
    {
        playerMovement.Rotate(playerInput.Horizontal);
    }

    private void FixedUpdate()
    {
        playerMovement.Move(playerInput.Horizontal);
    }

    private void OnJumpStarted()
    {
        playerAnimation.OnJumpStarted();
        ((MoveWithRigidbody)playerMovement).Jump();
    }

    private void OnJumpStopped()
    {
        playerAnimation.OnJumpStopped();
    }

    private void OnWalkStarted()
    {
        playerAnimation.OnWalkStarted();
    }

    private void OnWalkStopped()
    {
        playerAnimation.OnWalkStopped();
    }

    private void OnRunStarted()
    {
        playerAnimation.OnRunStarted();
        ((MoveWithRigidbody)playerMovement).speed = runningSpeed;
    }

    private void OnRunStopped()
    {
        playerAnimation.OnRunStopped();
        ((MoveWithRigidbody)playerMovement).speed = walkingSpeed;
    }

    private void OnCrouchStarted()
    {
        playerAnimation.OnCrouchStarted();
        ((MoveWithRigidbody)playerMovement).Crouch(true);
        initialSpeed = ((MoveWithRigidbody)playerMovement).speed;
        ((MoveWithRigidbody)playerMovement).speed = walkingSpeed;

    }

    private void OnCrouchStopped()
    {
        playerAnimation.OnCrouchStopped();
        ((MoveWithRigidbody)playerMovement).Crouch(false);
        ((MoveWithRigidbody)playerMovement).speed = initialSpeed;
    }
}
