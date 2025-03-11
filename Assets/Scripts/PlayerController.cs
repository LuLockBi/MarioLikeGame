using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{

    private int _health = 3;



    public TouchingDirections touchingDirections;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _collider;
    [SerializeField] private RuntimeAnimatorController _smallMarioController;
    [SerializeField] private AnimatorOverrideController _bigMarioOverrideController;
    [SerializeField] private AnimatorOverrideController _fireMarioOverrideController;
    [SerializeField] private Vector2 _bigMarioColliderSize = new(0.8f, 2f);
    private Vector2 _smallMarioColliderSize;

    [Header("Invincibility")]
    [SerializeField] private float _invincibilityDuration = 10f;
    [SerializeField] private float _blinkInterval = 0.2f;
    [SerializeField] private float _invincibilityTimer;

    [Header("Movement")]
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public float BounceForce = 3f;
    private Vector2 _moveInput;

    [Header("Fire")]
    [SerializeField] private float fireCooldown = 0.5f;
    [SerializeField] private Transform _firePoint;
    private float _fireTimer;

    [Header("States")]
    private bool _isMoving;
    private bool _isGrown = false;
    private bool _isDead = false;
    private bool _isFire = false;
    private bool _isInvincible = false;

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
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _smallMarioColliderSize = _collider.size;
        touchingDirections.OnGroundedChange += UpdateGroundedState;

        _fireTimer = fireCooldown;
    }

    private void Update()
    {

        Move();

        if (_fireTimer > 0)
        {
            _fireTimer -= Time.deltaTime;
        }
        UpdateInvincibility();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector2 hitDirection = contact.normal;

            if (hitDirection.y > 0.5f)
            {
                HandleEnemyKill(collision.gameObject.GetComponent<Enemy>());
            }
            else if (!_isInvincible)
            {
                TakeDamage();
            }
            else
            {
                HandleEnemyKill(collision.gameObject.GetComponent<Enemy>());
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
            if (!_isGrown)
            {
                Grow();
            }
        }
        else if (collision.gameObject.CompareTag("Flower"))
        {
            if (!_isFire && _isGrown)
            {
                FireMode();
            }
        }
        else if (collision.gameObject.CompareTag("Star"))
        {
            _isInvincible = true;
            _invincibilityTimer = _invincibilityDuration;
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
            }
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Fire();
        }
    }

    private void UpdateInvincibility()
    {
        if (_isInvincible)
        {
            _invincibilityTimer -= Time.deltaTime;
            float blink = Mathf.PingPong(Time.time, _blinkInterval) / _blinkInterval;
            _spriteRenderer.color = new Color(1, 1, 1, blink > 0.5f ? 1f : 0.5f);

            if (_invincibilityTimer <= 0)
            {
                _isInvincible = false;
                _spriteRenderer.color = Color.white;
                AudioManager.Instance.StopStarMusic();
            }
        }
    }

    private void Fire()
    {
        if (_isFire)
        {
            if (_fireTimer <= 0)
            {
                Vector2 direction = transform.localScale.x > 0f ? Vector2.right : Vector2.left;
                FireballPool.Instance.GetFireball(_firePoint.position, direction);
                _fireTimer = fireCooldown;
            }
        }
    }

    private void HandleEnemyKill(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage();
            if (!_isInvincible)
            {
                _rb.linearVelocityY = BounceForce;
            }
        }
    }

    private void Move()
    {

        if (touchingDirections.IsOnWall != true && !_isDead)
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
        _rb.linearVelocityY = JumpForce;
        _animator.SetTrigger("Jump");
        UnsecuredEventBus.TriggerPlayerJumped();
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
    private void FireMode()
    {
        _isFire = true;
        _animator.runtimeAnimatorController = _fireMarioOverrideController;
    }
    private void Die()
    {
        _isDead = true;
        _rb.linearVelocity = new Vector2(0, 10f);
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
