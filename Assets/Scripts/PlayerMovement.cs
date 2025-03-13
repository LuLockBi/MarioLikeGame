using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TouchingDirections),typeof(PlayerHealth))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _bounceForce = 3f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private TouchingDirections _tdirections;
    private PlayerHealth _health;
    private Vector2 _moveInput;
    private bool _isMoving;

    private float CurrentSpeed => _isMoving ? _moveSpeed : 0f;
    private bool IsMoving
    {
        get => _isMoving;
        set
        {
            if (value != _isMoving)
            {
                _isMoving = value;
                _animator.SetBool("isWalking", _isMoving);
            }
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _tdirections = GetComponent<TouchingDirections>();
        _health = GetComponent<PlayerHealth>();

        _tdirections.OnGroundedChange += UpdateGroundedState;
    }

    private void Update()
    {
        if (_health.IsDead) return;

        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _moveInput.y = 0f;
        IsMoving = _moveInput.x != 0;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && _tdirections.IsGrounded)
        {
            Jump();
        }
    }

    public void ApplyBounce()
    {
        _rb.linearVelocityY = _bounceForce;
    }

    private void Move()
    {
        if (_tdirections.IsOnWall != true)
        {
            _rb.linearVelocityX = _moveInput.x * CurrentSpeed;
        }
        if (_moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(_moveInput.x), 1, 1);
        }
    }

    private void Jump()
    {
        _rb.linearVelocityY = _jumpForce;
        _animator.SetTrigger("Jump");
        UnsecuredEventBus.TriggerPlayerJumped();
    }

    private void UpdateGroundedState(bool value)
    {
        _animator.SetBool("isGrounded", value);
    }

    private void OnDestroy()
    {
        _tdirections.OnGroundedChange -= UpdateGroundedState;
    }
}
