using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private AudioClip jumpSound;

    private int _health = 3;

    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public float BounceForce = 3f;

    public TouchingDirections touchingDirections;
    private Rigidbody2D rb;
    private Animator _animator;
    private CapsuleCollider2D _collider;
    [SerializeField] private RuntimeAnimatorController _smallMarioController;
    [SerializeField] private AnimatorOverrideController _bigMarioOverrideController;
    [SerializeField] private Vector2 _bigMarioColliderSize = new (1.2f, 2f);
    private Vector2 _smallMarioColliderSize;

    private Vector2 _moveInput;
    private bool _isMoving;
    private bool _isGrown = false;
    private bool _isDead;

    private float CurrentSpeed
    {
        get
        {
            if (_isMoving)
            {
                return MoveSpeed;
            }
            return 0f;
        }
    }


    private bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            if (value != _isMoving)
            {
                _isMoving = value;
                _animator.SetBool("isWalking", _isMoving);
            }
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();

        _smallMarioColliderSize = _collider.size;
        touchingDirections.OnGroundedChange += UpdateGroundedState;
    }

    private void Update()
    {

        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector2 hitDirection = contact.normal;

            if (hitDirection.y > 0.5f)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage();
                    rb.linearVelocityY = BounceForce;
                }
            }
            else
            {
                TakeDamage();
            }
        }

        else if (collision.gameObject.CompareTag("Block"))
        {
            ContactPoint2D contact = collision.contacts[0];
            if (contact.normal.y < 0)
            {
                Block block = collision.gameObject.GetComponent<Block>();
                if (block != null)
                {
                    BreakableBlock breakableBlock = block as BreakableBlock;
                    if (breakableBlock != null)
                    {
                        if (_isGrown)
                        {
                            breakableBlock.Hit();
                        }
                    }
                    else
                    {
                        block.Hit();
                    }
                }
            }
        }
        else if (collision.gameObject.CompareTag("Mushroom"))
        {
            Mushroom mushroom = collision.gameObject.GetComponent<Mushroom>();
            if (mushroom != null)
            {
                if (!_isGrown)
                {
                    Grow();
                }
            }
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        _moveInput.y = 0f;
        IsMoving = _moveInput.x != 0;

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (touchingDirections.IsGrounded)
            {
                Jump();
                AudioManager.Instance.PlaySound(jumpSound);
            }
        }
    }

    private void Move()
    {

        if (touchingDirections.IsOnWall != true && !_isDead)
        {
            rb.linearVelocityX = _moveInput.x * CurrentSpeed;
        }
        if (_moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(_moveInput.x), 1, 1);
        }
    }

    private void Jump()
    {
        rb.linearVelocityY = JumpForce;
        _animator.SetTrigger("Jump");
    }

    private void UpdateGroundedState(bool value)
    {
        _animator.SetBool("isGrounded", value);
    }

    private void OnDestroy()
    {
        touchingDirections.OnGroundedChange -= UpdateGroundedState;
    }

    private void TakeDamage()
    {
        _health--;
        if (_isGrown)
        {
            _isGrown = false;
            _collider.size = _smallMarioColliderSize;
            _animator.runtimeAnimatorController = _smallMarioController;
        }
        else
        {
            Die();
        }
    }

    private void Grow()
    {
        _isGrown = true;
        _collider.size = _bigMarioColliderSize;
        _animator.runtimeAnimatorController = _bigMarioOverrideController;
    }
    private void Die()
    {
        _isDead = true;
        rb.linearVelocity = new Vector2(0, 10f);
        _collider.enabled = false;
        _animator.SetTrigger("Die");
        UnsecuredEventBus.TriggerPlayerDied();
        ScoreManager.Instance.ResetScore();
        Invoke(nameof(RestartLevel), 4f);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
