using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }

    [SerializeField] private GameObject _uiCanvasPrefab;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _livesText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(_uiCanvasPrefab);
        }
        else
        {
            Destroy(gameObject);
            Destroy(_uiCanvasPrefab);

        }
        UpdateScoreText(0);
        UpdateLivesText(0);
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
        UnsecuredEventBus.OnHealthChanged += UpdateLivesText;
    }
    private void UnsubscribeFromEvents()
    {
        UnsecuredEventBus.OnScoreChanged -= UpdateScoreText;
        UnsecuredEventBus.OnCoinsChanged -= UpdateCoinText;
        UnsecuredEventBus.OnHealthChanged -= UpdateLivesText;
    }

    private void UpdateScoreText(int newScore)
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"Score {newScore:000000}";
        }
    }
    private void UpdateCoinText(int newCoins)
    {
        if (_coinText != null)
        {
            _coinText.text = $"Coins {newCoins:0}";
        }
    }
    private void UpdateLivesText(int lives)
    {
        if (_livesText != null)
        {
            _livesText.text = $"Lives {lives: 0}";
        }
    }

}
