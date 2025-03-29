using UnityEngine;

[CreateAssetMenu(fileName = "NoMovementSO", menuName = "Strategies/NoMovement")]
public class NoMovementSO : MovementStrategySO
{
    public override void Move(Rigidbody2D rb, Transform transform){}
}
