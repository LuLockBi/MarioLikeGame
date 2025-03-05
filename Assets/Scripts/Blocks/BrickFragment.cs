using UnityEngine;

public class BrickFragment : MonoBehaviour
{
    private readonly float _lifetime = 1f; 
    [SerializeField] private Rigidbody2D _rb;

    private void Start()
    {
        Destroy(gameObject, _lifetime); 
    }

    public void Initialize(Vector2 force)
    {
        //_rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = force;
        _rb.angularVelocity = Random.Range(-200f, 200f);
    }
}
