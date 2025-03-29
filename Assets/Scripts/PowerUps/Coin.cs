using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _lifetime = 0.5f;
    [SerializeField] private int _points = 100;
    private float _timer;

    private void OnEnable()
    {
        _timer = _lifetime;
        UnsecuredEventBus.TriggerCoinCollected(transform.position, _points);
    }

    private void Update()
    {
        transform.Translate(_moveSpeed * Time.deltaTime * Vector3.up);
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            CoinPool.Instance.ReturnCoin(this);
        }
    }
}
