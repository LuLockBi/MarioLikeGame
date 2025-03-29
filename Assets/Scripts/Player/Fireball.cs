using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _lifetime = 3f;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _direction;
    private float _timer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        

        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            FireballPool.Instance.ReturnFireball(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Enemy>().TakeDamage();
            FireballPool.Instance.ReturnFireball(this);
        }
    }
    public void Launch(Vector2 dir)
    {
        _direction = dir.normalized;
        _rb.linearVelocity = _direction * _speed;
        _timer = _lifetime;
        _spriteRenderer.flipX = _direction.x < 0;
    }
}
