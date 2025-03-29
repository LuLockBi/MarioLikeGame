using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInvincibility))]
public class PlayerState : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _smallMarioController;
    [SerializeField] private AnimatorOverrideController _bigMarioOverrideController;
    [SerializeField] private AnimatorOverrideController _fireMarioOverrideController;
    [SerializeField] private Vector2 _bigMarioColliderSize = new(0.8f, 2f);
    private Vector2 _smallMarioColliderSize;

    private bool _isGrown = false;
    private bool _isFire = false;
    private bool _isDead = false;

    private Animator _animator;
    private CapsuleCollider2D _collider;
    private Rigidbody2D _rb;
    private PlayerInvincibility _invincibility;

    [SerializeField] private float _weakInvincibilityDuration = 3f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _invincibility = GetComponent<PlayerInvincibility>();
        _smallMarioColliderSize = _collider.size;

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
    }

    public void TakeDamage()
    {
        if (_isDead || _invincibility.IsInvincible || _invincibility.IsWeakInvincible) return;

        if (_isGrown)
        {
            ShrinkWithWeakInvincibility();
        }
        else
        {
            Die();
        }
    }

    public void Grow()
    {
        if (_isDead || _isGrown) return;

        _isGrown = true;
        _collider.size = _bigMarioColliderSize;
        _animator.runtimeAnimatorController = _bigMarioOverrideController;
    }

    public void ActivateFireMode()
    {
        if (_isDead || !_isGrown || _isFire) return;

        _isFire = true;
        _animator.runtimeAnimatorController = _fireMarioOverrideController;
    }

    private void ShrinkWithWeakInvincibility()
    {
        _isGrown = false;
        _isFire = false;
        _collider.size = _smallMarioColliderSize;
        _animator.runtimeAnimatorController = _smallMarioController;
        _invincibility.ActivateWeakInvincibility(_weakInvincibilityDuration);

    }

    private void Die()
    {
        _isDead = true;
        _rb.linearVelocity = new Vector2(0, 10f);
        _collider.enabled = false;
        _animator.SetTrigger("Die");
        UnsecuredEventBus.TriggerPlayerDied();
    }

    public void DieInstantly()
    {
        if (_isDead) return;

        Die();
    }

    public void ResetParameters()
    {
        _isDead = false;
        _isGrown = false;
        _isFire = false;
        _collider.enabled = true;
        _collider.size = _smallMarioColliderSize;
        _animator.runtimeAnimatorController = _smallMarioController;
        _rb.position = Vector2.zero;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
    }

    public bool IsDead => _isDead;
    public bool IsGrown => _isGrown;
    public bool IsFire => _isFire;
}
