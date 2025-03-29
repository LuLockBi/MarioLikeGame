using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private int _points = 1000;
    private Rigidbody2D _rb;
    private bool _isMoving = false;
    private bool _isTurned = false;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(StartMoving), 1f);
    }

    private void Update()
    {
        if (_isMoving)
        {
            _rb.linearVelocityX = _moveSpeed;
        }
    }
    private void StartMoving()
    {
        _isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect();
        }
        else
        {
            Vector2 normal = collision.contacts[0].normal;
            if (Mathf.Abs(normal.x) > 0.5f)
            {
                _isTurned = !_isTurned;
                _moveSpeed = -_moveSpeed;
                this.GetComponent<SpriteRenderer>().flipX = _isTurned;
                _rb.linearVelocityX = _moveSpeed;
            }
        }
    }

    public void Collect()
    {
        UnsecuredEventBus.TriggerMushroomCollected(transform.position, _points);
        MushroomPool.Instance.ReturnMushroom(this);
    }
}
