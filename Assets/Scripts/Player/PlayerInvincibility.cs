using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerInvincibility : MonoBehaviour
{
    [SerializeField] private float _starInvincibilityDuration = 10f;
    [SerializeField] private float _blinkInterval = 0.2f;

    private float _invincibilityTimer;
    private bool _isStarInvincible = false; 
    private bool _isWeakInvincible = false;
    private SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateInvincibility();
    }

    public void ActivateStarInvincibility()
    {
        if (_isWeakInvincible) _isWeakInvincible = false; 

        _isStarInvincible = true;
        _invincibilityTimer = _starInvincibilityDuration;
    }

    public void ActivateWeakInvincibility(float duration)
    {
        if (_isWeakInvincible) return;

        IsWeakInvincible = true;
        _invincibilityTimer = duration;
    }

    private void UpdateInvincibility()
    {
        if (_isStarInvincible || _isWeakInvincible)
        {
            _invincibilityTimer -= Time.deltaTime;
            float blink = Mathf.PingPong(Time.time, _blinkInterval) / _blinkInterval;
            _spriteRenderer.color = new Color(1, 1, 1, blink > 0.5f ? 1f : 0.5f);


            if (_invincibilityTimer <= 0)
            {
                _isStarInvincible = false;
                IsWeakInvincible = false;
                _spriteRenderer.color = Color.white;
                if (!_isStarInvincible) 
                {
                    UnsecuredEventBus.TriggerInvincibilityEnded();
                }
            }
        }
    }

    public bool IsInvincible => _isStarInvincible;
    public bool IsWeakInvincible
    {
        get => _isWeakInvincible;
        set
        {
            if (value == _isWeakInvincible) return;

            _isWeakInvincible = value;
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), _isWeakInvincible);

        }
    }
        
}
