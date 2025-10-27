using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{

    public event Action OnPlayerJumped;




    [Header("References")]
    [SerializeField] private Transform orTransform;


    [Header("Movement Settings")]
    [SerializeField] private KeyCode movKey;
    [SerializeField] private float movSpeed;


    [Header("Jump Settings")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCoolDown;
    [SerializeField] private float airMultipleir;
    [SerializeField] private float airDrag;
    [SerializeField] private bool canJump;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode slideKey;
    [SerializeField] private float slideMultiplier;
    [SerializeField] private float slideDrag;

    [Header("Ground Check Settings")]
    [SerializeField] private float playerHight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDrag;

    private StateController stateController;
    private Rigidbody rb;
    private float startingMovSpeed, startingJumpForce;
    private float horInp, vertInp;
    private Vector3 movDirection;

    public bool isSliding;
    private void Awake()
    {
        stateController = GetComponent<StateController>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startingMovSpeed = movSpeed;
        startingJumpForce = jumpForce;
    }
    

   private void FixedUpdate() {

        SetPlayerMov();
        
    }

    private void Update() {

        SetState();
        SetInput();
        SetPlayerDrag();
        LimitPlayerSpeed();
        
    }

    private void SetInput()
    {
        horInp = Input.GetAxisRaw("Horizontal");
        vertInp = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey))
        {
            isSliding = true;
        }
        else if (Input.GetKeyDown(movKey))
        {

            isSliding = false;
        }
        else if (Input.GetKey(jumpKey) && canJump && IsGrounded())
        {
            canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), jumpCoolDown);
        }
    }
    
    private void SetState()
    {
        var movDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var isSliding = IsSliding();
        var currentState = stateController.GetCurrentState();



        var newState = currentState switch
        {
            _ when movDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when !canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };



        if (newState != currentState)
        {
            stateController.ChangeState(newState);
        }

    }


    private void SetPlayerMov()
    {
        movDirection = orTransform.forward * vertInp + orTransform.right * horInp;


        float forceMultiplier = stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => slideMultiplier,
            PlayerState.Jump => airMultipleir,
            _ => 1f
        };
        rb.AddForce(movDirection.normalized * movSpeed * forceMultiplier, ForceMode.Force);
    }
    private void SetPlayerDrag()
    {

        rb.linearDamping = stateController.GetCurrentState() switch
        {
            PlayerState.Move => groundDrag,
            PlayerState.Slide => slideDrag,
            PlayerState.Jump => airDrag,
            _ => rb.linearDamping
        };    
    }
    
    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if(flatVelocity.magnitude > movSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }


    private void SetPlayerJumping()
    {
        
        OnPlayerJumped?.Invoke();
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        
        rb.AddForce(orTransform.up * jumpForce, ForceMode.Impulse);
    }


    private void ResetJumping()
    {
        canJump = true;
    }


    #region Helper Functions



    private bool IsGrounded()
    {
        return Physics.Raycast(orTransform.position, Vector3.down, playerHight * 0.5f + 0.2f, groundLayer);
    }


    private Vector3 GetMovementDirection()
    {
        return movDirection.normalized;
    }

    private bool IsSliding()
    {
        return isSliding;
    }

    public void SetMovSpeed(float speed, float duration)
    {
        movSpeed += speed;
        Invoke(nameof(ResetMovSpeed), duration);
    }


    private void ResetMovSpeed()
    {
        movSpeed = startingMovSpeed;
    }


    public void SetJumpForce(float force, float duration)
    {
        jumpForce += force;
        Invoke(nameof(ResetJumpForce), duration);
    }


    private void ResetJumpForce()
    {
        jumpForce = startingJumpForce;
    }
        
    #endregion
}
