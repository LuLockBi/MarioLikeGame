using UnityEngine;

[CreateAssetMenu(menuName = "CollisionHandlers/EnemyCollision")]
public class EnemyCollisionHandler : CollisionHandler
{
    public override void HandleCollision(PlayerController player, Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Vector2 hitDirection = contact.normal;
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy == null) return;

        if (hitDirection.y > 0.5f)
        {
            HandleEnemyKill(player, enemy);
        }
        else if (!player.Invincibility.IsInvincible && !player.Invincibility.IsWeakInvincible)
        {
            player.State.TakeDamage();
        }
        else
        {
            HandleEnemyKill(player, enemy);
        }
    }

    private void HandleEnemyKill(PlayerController player, Enemy enemy)
    {
        enemy.TakeDamage();

        if (!player.Invincibility.IsInvincible && !player.Invincibility.IsWeakInvincible)
        {
            player.Movement.ApplyBounce();
        }
    }
}
