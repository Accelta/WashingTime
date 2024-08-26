using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputAction playerInputActions;
    private CharacterController characterController;
    private Vector2 moveInput;
    private Vector3 moveDirection;
    public float speed = 5.0f;
    public float jumpHeight = 5.0f;
    public float gravity = -9.81f;
    private bool isJumping;
    private bool isSprinting;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private void Awake()
    {
        playerInputActions = new PlayerInputAction();
        characterController = GetComponent<CharacterController>();

        // Menghubungkan input dengan event handler
        playerInputActions.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInputActions.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;

        playerInputActions.Gameplay.Jump.performed += ctx => Jump();
        playerInputActions.Gameplay.Sprint.performed += ctx => isSprinting = true;
        playerInputActions.Gameplay.Sprint.canceled += ctx => isSprinting = false;
        playerInputActions.Gameplay.Attack.performed += ctx => Attack();
    }

    private void OnEnable()
    {
        playerInputActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Gameplay.Disable();
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();
        MovePlayer();
    }

    private void MovePlayer()
    {
        float adjustedSpeed = isSprinting ? speed * 2.0f : speed;
        moveDirection.x = moveInput.x * adjustedSpeed;
        moveDirection.z = moveInput.y * adjustedSpeed;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("player jump!");
        }
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;


        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Attack()
    {
        Debug.Log("Attack performed!");
    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
    }
}
