using UnityEngine;

[RequireComponent(typeof(PlayerState))]
public class PlayerLevelEnd : MonoBehaviour
{
    [SerializeField] private Transform _castlePosition;
    [SerializeField] private float _moveToCastleSpeed = 4.5f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private PlayerState _health;
    private PlayerMovement _playerMovement;
    private bool _isFlagReached;
    private bool _isLevelEnding;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<PlayerState>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        UnsecuredEventBus.OnFlagReached += HandleFlagReached;
        UnsecuredEventBus.OnFlagLowered += HandleFlagLowered;
    }

    private void OnDisable()
    {
        UnsecuredEventBus.OnFlagReached -= HandleFlagReached;
        UnsecuredEventBus.OnFlagLowered -= HandleFlagLowered;
    }

    private void FixedUpdate()
    {
        if (_isLevelEnding)
        {
            MoveToCastle();
        }
    }

    private void MoveToCastle()
    {
        float targetX = _castlePosition.position.x;
        float currentX = transform.position.x;
        float distance = targetX - currentX;

        if (Mathf.Abs(distance) > 0.1f)
        {
            float moveDirection = Mathf.Sign(distance);
            _rb.linearVelocityX = moveDirection * _moveToCastleSpeed;
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
            _animator.SetBool("isWalking", false);
            gameObject.SetActive(false);
            UnsecuredEventBus.TriggerLevelCompleted();
        }
    }

    private void HandleFlagReached(Vector3 position, int points)
    {
        if (_health.IsDead) return;

        _playerMovement.enabled = false;
        _rb.linearVelocity = Vector2.zero;
        _isFlagReached = true;
    }

    private void HandleFlagLowered()
    {
        if (_health.IsDead) return;

        _isLevelEnding = true;
    }

    public bool IsFlagReached => _isFlagReached;
}
