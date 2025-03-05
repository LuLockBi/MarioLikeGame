using System;
using UnityEngine;

public static class UnsecuredEventBus
{

    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnCoinsChanged;
    public static event Action<Vector3, int> OnEnemyKilled;
    public static event Action<Vector3, int> OnBlockDestroyed;
    public static event Action<Vector3, int> OnCoinCollected;
    public static event Action<Vector3, int> OnMushroomCollected;
    public static event Action OnPlayerDied;
    public static event Action<GameObject> OnUILoaded;

    public static void TriggerScoreChanged(int newScore)
    {
        OnScoreChanged?.Invoke(newScore);
    }
    public static void TriggerCoinsChanged(int newCoinCount)
    {
        OnCoinsChanged?.Invoke(newCoinCount);
    }

    public static void TriggerEnemyKilled(Vector3 position, int points)
    {
        OnEnemyKilled?.Invoke(position, points);
    }
    public static void TriggerBlockDestroyed(Vector3 position, int points)
    {
        OnBlockDestroyed?.Invoke(position, points);
    }public static void TriggerCoinCollected(Vector3 position, int points)
    {
        OnCoinCollected?.Invoke(position, points);
    }public static void TriggerMushroomCollected(Vector3 position, int points)
    {
        OnMushroomCollected?.Invoke(position, points);
    }
    public static void TriggerPlayerDied()
    {
        OnPlayerDied?.Invoke();
    }
    public static void TriggerUILoaded(GameObject uiRoot)
    {
        OnUILoaded?.Invoke(uiRoot);
    }
}
