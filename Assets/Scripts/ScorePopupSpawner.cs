using UnityEngine;

public class ScorePopupSpawner : MonoBehaviour
{
    public static ScorePopupSpawner Instance { get; private set; }

    [SerializeField] private ScorePopupPool _popupPool;
    [SerializeField] private Vector3 _popupOffset = new(0, -3f, 0);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (_popupPool == null) Debug.LogError($"_popupPool в {this} не установлен");
    }

    private void OnEnable()
    {
        UnsecuredEventBus.OnEnemyKilled += HandleEnemyKilled;
        UnsecuredEventBus.OnBlockDestroyed += HandleBlockDestroyed;
        UnsecuredEventBus.OnCoinCollected += HandleCoinCollected;
        UnsecuredEventBus.OnMushroomCollected += HandleMushroomCollected;
        UnsecuredEventBus.OnFlowerCollected += HandleFlowerCollected;
        UnsecuredEventBus.OnStarCollected += HandleStarCollected;
        UnsecuredEventBus.OnFlagReached += HandleFlagReached;
    }

    private void OnDisable()
    {
        UnsecuredEventBus.OnEnemyKilled -= HandleEnemyKilled;
        UnsecuredEventBus.OnBlockDestroyed -= HandleBlockDestroyed;
        UnsecuredEventBus.OnCoinCollected -= HandleCoinCollected;
        UnsecuredEventBus.OnMushroomCollected -= HandleMushroomCollected;
        UnsecuredEventBus.OnFlowerCollected -= HandleFlowerCollected;
        UnsecuredEventBus.OnStarCollected -= HandleStarCollected;
        UnsecuredEventBus.OnFlagReached -= HandleFlagReached;
    }

    private void HandleEnemyKilled(Vector3 position, int points)
    {
        _popupPool.GetPopup(points, position + _popupOffset);
    }

    private void HandleBlockDestroyed(Vector3 position, int points)
    {
        _popupPool.GetPopup(points, position + _popupOffset);
    }

    private void HandleCoinCollected(Vector3 position, int points)
    {
        _popupPool.GetPopup(points, position + _popupOffset);
    }

    private void HandleMushroomCollected(Vector3 position, int points)
    {
        _popupPool.GetPopup(points, position + _popupOffset);
    }

    private void HandleFlowerCollected(Vector3 position, int points)
    {
        _popupPool.GetPopup(points, position + _popupOffset);
    }

    private void HandleStarCollected(Vector3 position, int points)
    {
        _popupPool.GetPopup(points, position + _popupOffset);
    }

    private void HandleFlagReached(Vector3 position, int points)
    {
        _popupPool.GetPopup(points, position + _popupOffset);
    }
}
