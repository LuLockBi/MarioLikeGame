using UnityEngine;

[CreateAssetMenu(menuName = "CollisionHandlers/PowerUpCollision")]
public class PowerUpCollisionHandler : CollisionHandler
{
    public override void HandleCollision(PlayerController player, Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Mushroom":
                player.State.Grow();
                break;

            case "Flower":
                player.State.ActivateFireMode();
                break;

            case "Star":
                player.Invincibility.ActivateStarInvincibility();
                break;
        }
    }
}
