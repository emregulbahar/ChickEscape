using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform orTransform;


    [Header("Movement Settings")]
    [SerializeField] private KeyCode movKey;
    [SerializeField] private float movSpeed;


    [Header("Jump Settings")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCoolDown;
    [SerializeField] private bool canJump;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode slideKey;
    [SerializeField] private float slideMultiplier;
    [SerializeField] private float slideDrag;

    [Header("Ground Check Settings")]
    [SerializeField] private float playerHight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDrag;


    private Rigidbody rb;

    private float horInp, vertInp;
    private Vector3 movDirection;

    private bool isSliding;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    

   private void FixedUpdate() {

        SetPlayerMov();
        
    }

    private void Update() {

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


    private void SetPlayerMov()
    {
        movDirection = orTransform.forward * vertInp + orTransform.right * horInp;
        if (isSliding)
        {
            rb.AddForce(movDirection.normalized * movSpeed * slideMultiplier, ForceMode.Force);
        }
        else
        {
            rb.AddForce(movDirection.normalized * movSpeed, ForceMode.Force);
        }
    }

    private void SetPlayerDrag()
    {
        if (isSliding)
        {
            rb.linearDamping = slideDrag;
        }
        else
        {
            rb.linearDamping = groundDrag;
        }
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
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        
        rb.AddForce(orTransform.up * jumpForce, ForceMode.Impulse);
    }


    private void ResetJumping()
    {
        canJump = true;
    }
    

    private bool IsGrounded()
    {
        return Physics.Raycast(orTransform.position, Vector3.down, playerHight * 0.5f + 0.2f, groundLayer);
    }

}
