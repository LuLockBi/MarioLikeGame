using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _enemyDeathSound;
    [SerializeField] private AudioClip _blockBreakSound;
    [SerializeField] private AudioClip _mushroomSound;
    [SerializeField] private AudioClip _coinSound;
    [SerializeField] private AudioClip _flowerSound;
    [SerializeField] private AudioClip _starSound;
    [SerializeField] private AudioClip _playerDeathSound;
    [SerializeField] private AudioClip _playerJumpSound;
    [SerializeField] private AudioClip _togglePauseSound;
    

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
        UnsecuredEventBus.OnFlowerCollected += HandleFlowerCollected;
        UnsecuredEventBus.OnStarCollected += HandleStarCollected;
        UnsecuredEventBus.OnBlockDestroyed += HandleBlockDestroyed;
        UnsecuredEventBus.OnEnemyKilled += HandleEnemyKilled;
        UnsecuredEventBus.OnPlayerDied += HandlePlayerDied;
        UnsecuredEventBus.OnPlayerJumped += HandlePlayerJumped;
        UnsecuredEventBus.OnPauseToggle += HandlePauseToggle;
    }
    private void UnsubscribeFromEvents()
    {
        UnsecuredEventBus.OnCoinCollected -= HandleCoinCollected;
        UnsecuredEventBus.OnMushroomCollected -= HandleMushroomCollected;
        UnsecuredEventBus.OnFlowerCollected -= HandleFlowerCollected;
        UnsecuredEventBus.OnStarCollected -= HandleStarCollected;
        UnsecuredEventBus.OnBlockDestroyed -= HandleBlockDestroyed;
        UnsecuredEventBus.OnEnemyKilled -= HandleEnemyKilled;
        UnsecuredEventBus.OnPlayerDied -= HandlePlayerDied;
        UnsecuredEventBus.OnPlayerJumped -= HandlePlayerJumped;
        UnsecuredEventBus.OnPauseToggle -= HandlePauseToggle;
    }

    private void HandleCoinCollected(Vector3 position, int points)
    {
        AudioManager.Instance.PlaySound(_coinSound, 1f);
    }
    private void HandleMushroomCollected(Vector3 position, int points)
    {
        AudioManager.Instance.PlaySound(_mushroomSound, 1f);
    }
    private void HandleFlowerCollected(Vector3 position, int points)
    {
        AudioManager.Instance.PlaySound(_flowerSound, 1f);
    }
    private void HandleStarCollected(Vector3 position, int points)
    {
        AudioManager.Instance.PlayStarMusic(_starSound, 0.5f);
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
        AudioManager.Instance.PlaySound(_playerDeathSound, 1f);
    }
    private void HandlePlayerJumped()
    {
        AudioManager.Instance.PlaySound(_playerJumpSound, 1f);
    }
    private void HandlePauseToggle()
    {
        AudioManager.Instance.PlaySound(_togglePauseSound, 1f);
    }
}
