using System;
using UnityEngine;
using UnityEngine.UI;

public static class UnsecuredEventBus
{
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnCoinsChanged;
    public static event Action<int> OnHealthChanged;
    public static event Action<Vector3, int> OnEnemyKilled;
    public static event Action<Vector3, int> OnBlockDestroyed;
    public static event Action<Vector3, int> OnCoinCollected;
    public static event Action<Vector3, int> OnMushroomCollected;
    public static event Action<Vector3, int> OnFlowerCollected;
    public static event Action<Vector3, int> OnStarCollected;
    public static event Action<Vector3, int> OnFlagReached;
    public static event Action OnLevelCompleted;
    public static event Action OnFlagLowered;
    public static event Action OnInvincibilityEnded;
    public static event Action OnPlayerJumped;
    public static event Action OnPlayerDied;
    public static event Action<GameObject> OnUILoaded;
    public static event Action OnPauseToggle;
    public static event Action OnGameLose;
    public static event Action OnPlayerFire;

    public static void TriggerScoreChanged(int newScore)
    {
        OnScoreChanged?.Invoke(newScore);
    }
    public static void TriggerCoinsChanged(int newCoinCount)
    {
        OnCoinsChanged?.Invoke(newCoinCount);
    }
    public static void TriggerHealthChanged(int newLives)
    {
        OnHealthChanged?.Invoke(newLives);
    }

    public static void TriggerEnemyKilled(Vector3 position, int points)
    {
        OnEnemyKilled?.Invoke(position, points);
    }
    public static void TriggerBlockDestroyed(Vector3 position, int points)
    {
        OnBlockDestroyed?.Invoke(position, points);
    }
    public static void TriggerCoinCollected(Vector3 position, int points)
    {
        OnCoinCollected?.Invoke(position, points);
    }
    public static void TriggerMushroomCollected(Vector3 position, int points)
    {
        OnMushroomCollected?.Invoke(position, points);
    }
    public static void TriggerFlowerCollected(Vector3 position, int points)
    {
        OnFlowerCollected?.Invoke(position, points);
    }
    public static void TriggerStarCollected(Vector3 position, int points)
    {
        OnStarCollected?.Invoke(position, points);
    }
    public static void TriggerPlayerDied()
    {
        OnPlayerDied?.Invoke();
    }
    public static void TriggerPlayerJumped()
    {
        OnPlayerJumped?.Invoke();
    }
    public static void TriggerUILoaded(GameObject uiRoot)
    {
        OnUILoaded?.Invoke(uiRoot);
    }
    public static void TriggerPauseToggle()
    {
        OnPauseToggle?.Invoke();
    }
    public static void TriggerInvincibilityEnded()
    {
        OnInvincibilityEnded?.Invoke();
    }
    public static void TriggerFlagLowered()
    {
        OnFlagLowered?.Invoke();
    }
    public static void TriggerFlagReached(Vector3 position, int points)
    {
        OnFlagReached?.Invoke(position, points);
    }
    public static void TriggerLevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
    public static void TriggerGameLose()
    {
        OnGameLose?.Invoke();
    }
    public static void TriggerPlayerFire()
    {
        OnPlayerFire?.Invoke();
    }
}
