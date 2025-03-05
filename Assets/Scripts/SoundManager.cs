using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _enemyDeathSound;
    [SerializeField] private AudioClip _blockBreakSound;
    [SerializeField] private AudioClip _mushroomSound;
    [SerializeField] private AudioClip _coinSound;
    [SerializeField] private AudioClip _playerDeathSound;
    public static SoundManager Instance { get; private set; }

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
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        UnsecuredEventBus.OnCoinCollected += HandleCoinCollected;
        UnsecuredEventBus.OnMushroomCollected += HandleMushroomCollected;
        UnsecuredEventBus.OnBlockDestroyed += HandleBlockDestroyed;
        UnsecuredEventBus.OnEnemyKilled += HandleEnemyKilled;
        UnsecuredEventBus.OnPlayerDied += HandlePlayerDied;
    }
    private void UnsubscribeFromEvents()
    {
        UnsecuredEventBus.OnCoinCollected -= HandleCoinCollected;
        UnsecuredEventBus.OnMushroomCollected -= HandleMushroomCollected;
        UnsecuredEventBus.OnBlockDestroyed -= HandleBlockDestroyed;
        UnsecuredEventBus.OnEnemyKilled -= HandleEnemyKilled;
        UnsecuredEventBus.OnPlayerDied -= HandlePlayerDied;
    }

    private void HandleCoinCollected(Vector3 position, int points)
    {
        AudioManager.Instance.PlaySound(_coinSound, 1f);
    }

    private void HandleMushroomCollected(Vector3 position, int points)
    {
        AudioManager.Instance.PlaySound(_mushroomSound, 1f);
    }

    private void HandleBlockDestroyed(Vector3 position, int points)
    {
        AudioManager.Instance.PlaySound(_blockBreakSound, 1f);
    }

    private void HandleEnemyKilled(Vector3 position, int points)
    {
        AudioManager.Instance.PlaySound(_enemyDeathSound, 1f);
    }

    private void HandlePlayerDied()
    {
        AudioManager.Instance.PlaySound(_playerDeathSound,1f);
    }
}
