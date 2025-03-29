using UnityEngine;

public class MushroomPool : MonoBehaviour
{
    public static MushroomPool Instance { get; private set; }
    [SerializeField] private Mushroom _prefab;
    [SerializeField] private int _initSize = 2;
    [SerializeField] private bool _autoExpand = true;
    private ObjectPool<Mushroom> _pool;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _pool = new ObjectPool<Mushroom>(_prefab, _initSize, transform, _autoExpand);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Mushroom GetMushroom(Vector3 position)
    {
        return _pool.GetFreeElement(position);
    }

    public void ReturnMushroom(Mushroom mushroom)
    {
        _pool.ReturnToPool(mushroom);
    }
}
