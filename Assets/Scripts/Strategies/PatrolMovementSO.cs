using UnityEngine;

[CreateAssetMenu(fileName = "PatrolMovementSO", menuName = "Strategies/PatrolMovement")]
public class PatrolMovementSO : MovementStrategySO
{
    public float speed = 2f;
    public float patrolDistance = 2f;
    private Vector2 startPosition;

    public override void Move(Rigidbody2D rb, Transform transform)
    {
        if (startPosition == Vector2.zero) startPosition = transform.position;

        float direction = Mathf.Sin(Time.time * speed);
        rb.linearVelocityX = direction * speed;

        if (Vector2.Distance(startPosition, transform.position) > patrolDistance)
        {
            rb.linearVelocityX = -rb.linearVelocityX;
        }
    }
}
