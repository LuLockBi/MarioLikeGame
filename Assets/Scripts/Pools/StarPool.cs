using UnityEngine;

public class StarPool : MonoBehaviour
{
    public static StarPool Instance;
    [SerializeField] private Star _starPrefab;
    [SerializeField] private int _count = 2;
    [SerializeField] private bool _autoExpand = true;
    private ObjectPool<Star> _pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _pool = new ObjectPool<Star>(_starPrefab, _count, transform, _autoExpand);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Star GetStar(Vector3 position)
    {
        return _pool.GetFreeElement(position);
    }
    public void ReturnStar(Star star)
    {
        _pool.ReturnToPool(star);
    }
}
