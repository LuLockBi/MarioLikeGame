using UnityEngine;

public class FlowerPool : MonoBehaviour
{
    public static FlowerPool Instance;
    [SerializeField] private Flower _flowerPrefab;
    [SerializeField] private int _count = 2;
    [SerializeField] private bool _autoExpand = true;

    private ObjectPool<Flower> _pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _pool = new ObjectPool<Flower>(_flowerPrefab, _count, transform, _autoExpand);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Flower GetFlower(Vector3 position)
    {
        return _pool.GetFreeElement(position);
    }

    public void ReturnFlower(Flower flower)
    {
        _pool.ReturnToPool(flower);
    }
}
