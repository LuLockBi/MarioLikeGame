using UnityEngine;

public abstract class CollisionHandler : ScriptableObject
{
    public abstract void HandleCollision(PlayerController player, Collision2D collision);
}
