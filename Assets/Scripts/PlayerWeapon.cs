using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float _fireCooldown = 0.5f;
    [SerializeField] private Transform _firePoint;

    private PlayerHealth _health;
    private float _fireTimer;

    private void Awake()
    { 
        _health = GetComponent<PlayerHealth>();
        _fireTimer = _fireCooldown;
    }

    private void Update()
    {
        if (_fireTimer > 0)
        {
            _fireTimer -= Time.deltaTime;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (_health.IsFire && _fireTimer <= 0)
        {
            Vector2 direction = transform.localScale.x > 0f ? Vector2.right : Vector2.left;
            FireballPool.Instance.GetFireball(_firePoint.position, direction);
            _fireTimer = _fireCooldown;
            UnsecuredEventBus.TriggerPlayerFire();
        }
    }
}
