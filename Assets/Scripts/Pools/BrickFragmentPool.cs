using UnityEngine;

public class BrickFragmentPool : MonoBehaviour
{
    public static BrickFragmentPool Instance;

    [SerializeField] private BrickFragment _leftPrefab;
    [SerializeField] private BrickFragment _rightPrefab;

    [SerializeField] private int _initSize = 6;
    [SerializeField] private bool _autoExpand = true;
    private ObjectPool<BrickFragment> _leftPool;
    private ObjectPool<BrickFragment> _rightPool;


    private Vector2 _leftCashForce = Vector2.zero;
    private Vector2 _rightCashForce = Vector2.zero;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _leftPool = new ObjectPool<BrickFragment>(_leftPrefab, _initSize, transform, _autoExpand,
                onGet: fragment => fragment.Initialize(_leftCashForce));
            _rightPool = new ObjectPool<BrickFragment>(_rightPrefab,_initSize,transform,_autoExpand,
                onGet: fragment => fragment.Initialize(_rightCashForce));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public BrickFragment GetLeftFragment(Vector3 position, Vector2 force)
    {
        _leftCashForce = force;
        return _leftPool.GetFreeElement(position);
    }
    public BrickFragment GetRightFragment(Vector3 position, Vector2 force)
    {
        _rightCashForce = force;
        return _rightPool.GetFreeElement(position);
    }
    public void ReturnLeftFragment(BrickFragment fragment)
    {
        _leftPool.ReturnToPool(fragment);
    }
    public void ReturnRightFragment(BrickFragment fragment)
    {
        _rightPool.ReturnToPool(fragment);
    }
}
