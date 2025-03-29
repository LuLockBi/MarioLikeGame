using UnityEngine;

public class FireballPool : MonoBehaviour
{
    public static FireballPool Instance;
    [SerializeField] private Fireball _fireballPrefab;
    [SerializeField] private int _count = 5;
    [SerializeField] private bool _autoExpand = true;
    private ObjectPool<Fireball> _pool;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _pool = new ObjectPool<Fireball>(_fireballPrefab, _count, transform, _autoExpand);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Fireball GetFireball(Vector3 position, Vector2 direction)
    {
        var fireball = _pool.GetFreeElement(position);
        fireball.Launch(direction);
        return fireball;
    }
    public void ReturnFireball(Fireball fireball)
    {
        _pool.ReturnToPool(fireball);
    }
}
