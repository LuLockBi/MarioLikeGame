using UnityEngine;

public abstract class MovementStrategySO : ScriptableObject
{
    public abstract void Move(Rigidbody2D rb, Transform transform);
}
