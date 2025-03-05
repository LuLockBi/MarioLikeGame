using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private MovementStrategySO _movementStrategy; 
    [SerializeField] private AttackStrategySO _attackStrategy;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_movementStrategy != null)
            _movementStrategy.Move(_rb, transform);
        if(_attackStrategy != null)
            _attackStrategy.Attack(transform);
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
        UnsecuredEventBus.TriggerEnemyKilled(transform.position, 100);
    }

}
