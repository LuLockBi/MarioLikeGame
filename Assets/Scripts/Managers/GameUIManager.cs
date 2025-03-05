using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }

    [SerializeField] private ScorePopupPool _popupPool;
    [SerializeField] private Vector3 _popupOffset = new (0, -3f, 0);

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinText;

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
        UpdateScoreText(0);
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
        UnsecuredEventBus.OnScoreChanged += UpdateScoreText;
        UnsecuredEventBus.OnCoinsChanged += UpdateCoinText;
        UnsecuredEventBus.OnEnemyKilled += HandleEnemyKilled;
        UnsecuredEventBus.OnBlockDestroyed += HandleBlockDestroyed;
        UnsecuredEventBus.OnCoinCollected += HandleCoinCollected;
        UnsecuredEventBus.OnMushroomCollected += HandleMushroomCollected;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void UnsubscribeFromEvents()
    {
        UnsecuredEventBus.OnScoreChanged -= UpdateScoreText;
        UnsecuredEventBus.OnCoinsChanged -= UpdateCoinText;
        UnsecuredEventBus.OnEnemyKilled -= HandleEnemyKilled;
        UnsecuredEventBus.OnBlockDestroyed -= HandleBlockDestroyed;
        UnsecuredEventBus.OnCoinCollected -= HandleCoinCollected;
        UnsecuredEventBus.OnMushroomCollected -= HandleMushroomCollected;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindUIElements();
    }

    private void BindUIElements()
    {
        GameObject uiContainer = GameObject.Find("===UI==="); 
        if (uiContainer == null)
        {
            Debug.LogError("Контейнер не найден в сцене");
            return;
        }

        Transform gameUITransform = uiContainer.transform.Find("GameUI");
        if (gameUITransform == null)
        {
            Debug.LogError("GameUI не найден в Контейнер/UI");
            return;
        }

        Transform infoPanel = gameUITransform.Find("InfoPanel");
        if (infoPanel != null)
        {
            _scoreText = infoPanel.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
            _coinText = infoPanel.Find("CoinsText")?.GetComponent<TextMeshProUGUI>();
        }

        if (_scoreText == null) Debug.LogError("ScoreText не найден в InfoPanel");
        if (_coinText == null) Debug.LogError("CoinsText не найден в InfoPanel");

        if (_scoreText != null) UpdateScoreText(ScoreManager.Instance.GetScore());
        if (_coinText != null) UpdateCoinText(ScoreManager.Instance.GetCoins());
    }

    private void UpdateScoreText(int newScore)
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"Score: {newScore:000000}";
        }
    }private void UpdateCoinText(int newCoins)
    {
        if (_coinText != null)
        {
            _coinText.text = $"Coins: {newCoins:0}";
        }
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

}
