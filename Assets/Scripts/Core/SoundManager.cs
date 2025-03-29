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
    [SerializeField] private AudioClip _FlagLoweringSound;
    [SerializeField] private AudioClip _fireSound;
    
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
    private void OnDestroy()
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
        UnsecuredEventBus.OnInvincibilityEnded += HandleInvincibilityEnded;
        UnsecuredEventBus.OnFlagReached += HandleFlagReached;
        UnsecuredEventBus.OnPlayerFire += HandlePlayerFire;
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
        UnsecuredEventBus.OnInvincibilityEnded -= HandleInvincibilityEnded;
        UnsecuredEventBus.OnFlagReached -= HandleFlagReached;
        UnsecuredEventBus.OnPlayerFire -= HandlePlayerFire;
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
        AudioManager.Instance.StopAllMusic();
    }
    private void HandlePlayerJumped()
    {
        AudioManager.Instance.PlaySound(_playerJumpSound, 1f);
    }
    private void HandlePauseToggle()
    {
        AudioManager.Instance.PlaySound(_togglePauseSound, 1f);
    }
    private void HandleStarCollected(Vector3 position, int points)
    {
        AudioManager.Instance.PauseBackgroundMusic();
        AudioManager.Instance.PlayStarMusic(_starSound, 0.5f);
    }
    private void HandleInvincibilityEnded()
    {
        AudioManager.Instance.StopStarMusic();
    }
    private void HandleFlagReached(Vector3 position, int points)
    {
        AudioManager.Instance.PlaySound(_FlagLoweringSound, 1f);
    }
    private void HandlePlayerFire()
    {
        AudioManager.Instance.PlaySound(_fireSound, 1f);
    }
}
