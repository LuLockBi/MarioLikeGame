
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int _score = 0;
    private int _coins = 0;

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
        UnsecuredEventBus.OnEnemyKilled += HandleEnemyKilled;
        UnsecuredEventBus.OnCoinCollected += HandleCoinCollected;
        UnsecuredEventBus.OnMushroomCollected += HandleMushroomCollected;
        UnsecuredEventBus.OnBlockDestroyed += HandleBlockDestroyed;
    }

    private void UnsubscribeFromEvents()
    {
        UnsecuredEventBus.OnEnemyKilled -= HandleEnemyKilled;
        UnsecuredEventBus.OnCoinCollected -= HandleCoinCollected;
        UnsecuredEventBus.OnMushroomCollected -= HandleMushroomCollected;
        UnsecuredEventBus.OnBlockDestroyed -= HandleBlockDestroyed;
    }


    public void AddScore(int points)
    {
        _score += points;
        UnsecuredEventBus.TriggerScoreChanged(_score);
    }

    public void ResetScore()
    {
        _score = 0;
        _coins = 0; 
        UnsecuredEventBus.TriggerScoreChanged(_score);
        UnsecuredEventBus.TriggerCoinsChanged(_coins);
    }

    public int GetScore()
    {
        return _score;
    }
    public int GetCoins()
    {
        return _coins;
    }

    private void HandleEnemyKilled(Vector3 position, int points)
    {
        AddScore(points);
    }
    private void HandleCoinCollected(Vector3 position, int points)
    {
        AddScore(points);
        AddCoin();
    }

    private void AddCoin()
    {
        _coins += 1;
        UnsecuredEventBus.TriggerCoinsChanged(_coins);
    }

    private void HandleMushroomCollected(Vector3 position, int points)
    {
        AddScore(points);
    }
    private void HandleBlockDestroyed(Vector3 position, int points)
    {
        AddScore(points);
    }
}
