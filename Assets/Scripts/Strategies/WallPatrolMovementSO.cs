using UnityEngine;


[CreateAssetMenu(fileName = "WallPatrolMovementSO", menuName = "Strategies/WallPatrolMovement")]
public class WallPatrolMovementSO : MovementStrategySO
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private float _rayDistance = 0.1f;

    public Vector2 Offset = Vector2.zero;

    private int direction = 1;

    public override void Move(Rigidbody2D rb, Transform transform)
    {
        rb.linearVelocityX = direction * _speed;

        Vector2 rayOrigin = (Vector2)transform.position + Offset;
        RaycastHit2D hitLeft = Physics2D.Raycast(rayOrigin, Vector2.left, _rayDistance, _wallLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(rayOrigin, Vector2.right, _rayDistance, _wallLayer);

        Debug.DrawRay(rayOrigin, Vector2.left * _rayDistance, Color.red);
        Debug.DrawRay(rayOrigin, Vector2.right * _rayDistance, Color.red);


        if (hitLeft.collider != null && direction == -1) 
        {
            direction = 1; 
        }
        else if (hitRight.collider != null && direction == 1)
        {
            direction = -1; 
        }
    }
}
