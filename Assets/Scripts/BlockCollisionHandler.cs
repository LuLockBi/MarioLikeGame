using UnityEngine;

[CreateAssetMenu(menuName = "CollisionHandlers/BlockCollision")]
public class BlockCollisionHandler : CollisionHandler
{
    public override void HandleCollision(PlayerController player, Collision2D collision)
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
                    if (player.State.IsGrown)
                        breakableBlock.Hit();
                }
                else
                {
                    block.Hit();
                }
            }
        }
    }
}
