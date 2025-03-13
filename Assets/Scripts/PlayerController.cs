using UnityEngine;

[RequireComponent(typeof(TouchingDirections), typeof(PlayerInvincibility),typeof(PlayerLevelEnd))]
public class PlayerController : MonoBehaviour
{
    private PlayerHealth _health;
    private PlayerInvincibility _invincibility;
    private PlayerMovement _movement;

    private void Start()
    {
        _health = GetComponent<PlayerHealth>();
        _invincibility = GetComponent<PlayerInvincibility>();
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
            else if (!_invincibility.IsInvincible && !_invincibility.IsWeakInvincible)
            {
                _health.TakeDamage();
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
                        if (_health.IsGrown)
                            breakableBlock.Hit();
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
            _health.Grow();
        }
        else if (collision.gameObject.CompareTag("Flower"))
        {
            _health.ActivateFireMode();
        }
        else if (collision.gameObject.CompareTag("Star"))
        {
            _invincibility.ActivateStarInvincibility();
        }
    }

    private void HandleEnemyKill(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage();
            if (!_invincibility.IsInvincible && !_invincibility.IsWeakInvincible)
            {
                _movement.ApplyBounce();
            }
        }
    }
}