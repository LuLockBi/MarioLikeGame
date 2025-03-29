using UnityEngine;

[CreateAssetMenu(menuName = "CollisionHandlers/CollisionManager")]
public class CollisionManager : ScriptableObject
{
    [SerializeField] private CollisionHandler _enemyCollisionHandler;
    [SerializeField] private CollisionHandler _blockCollisionHandler;
    [SerializeField] private CollisionHandler _powerUpCollisionHandler;

    public void HandleCollision(PlayerController player, Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        switch (tag)
        {
            case "Enemy":
                _enemyCollisionHandler?.HandleCollision(player, collision);
                break;
            case "Block":
                _blockCollisionHandler?.HandleCollision(player, collision);
                break;
            case "Mushroom":
            case "Flower":
            case "Star":
                _powerUpCollisionHandler?.HandleCollision(player, collision);
                break;
            default:
                break;
        }
    }
}
