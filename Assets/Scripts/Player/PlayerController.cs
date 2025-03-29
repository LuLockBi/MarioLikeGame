using UnityEngine;

[RequireComponent(typeof(TouchingDirections), typeof(PlayerInvincibility), typeof(PlayerLevelEnd))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CollisionManager _collisionManager;
    public PlayerState State { get; private set; }
    public PlayerInvincibility Invincibility { get; private set; }
    public PlayerMovement Movement { get; private set; }

    private void Start()
    {
        Initialize(GetComponent<PlayerState>(), GetComponent<PlayerInvincibility>(), GetComponent<PlayerMovement>());

        if (_collisionManager == null)
        {
            Debug.LogError("CollisionManager не назначен в PlayerController");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collisionManager?.HandleCollision(this, collision);
    }

    public void Initialize(PlayerState health, PlayerInvincibility invincibility, PlayerMovement movement)
    {
        State = health;
        Invincibility = invincibility;
        Movement = movement;
    }
}