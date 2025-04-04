using UnityEngine;

public class BrickFragment : MonoBehaviour
{
    private readonly float _lifetime = 1f; 
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private bool _isLeftFragment;

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), _lifetime);
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(ReturnToPool));
    }

    public void Initialize(Vector2 force)
    {
        //_rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = force;
        _rb.angularVelocity = Random.Range(-200f, 200f);
    }

    private void ReturnToPool()
    {
        if (_isLeftFragment)
        {
            BrickFragmentPool.Instance.ReturnLeftFragment(this);
        }
        else
        {
            BrickFragmentPool.Instance.ReturnRightFragment(this);
        }
    }
    
}
