using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public static CoinPool Instance { get; private set; }
    [SerializeField] private Coin _prefab;
    [SerializeField] private int _initSize = 10;
    [SerializeField] private bool _autoExpand = true;
    private ObjectPool<Coin> _pool;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _pool = new ObjectPool<Coin>(_prefab, _initSize, transform, _autoExpand);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Coin GetCoin(Vector3 position)
    {
        return _pool.GetFreeElement(position);
    }

    public void ReturnCoin(Coin coin)
    {
        _pool.ReturnToPool(coin);
    }
}
