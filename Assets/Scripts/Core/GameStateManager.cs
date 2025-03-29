using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [SerializeField] private int _maxLives = 3;
    [SerializeField] private PlayerState _playerState; 
    [SerializeField] private float _restartDelay = 4f; 
    [SerializeField] private string _firstLevelScene = "Level_1-1";

    private int _lives;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _lives = _maxLives;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        UnsecuredEventBus.OnPlayerDied += HandlePlayerDied;
        UnsecuredEventBus.OnLevelRestarted += HandleGameRestarted;
    }

    private void OnDisable()
    {
        UnsecuredEventBus.OnPlayerDied -= HandlePlayerDied;
        UnsecuredEventBus.OnLevelRestarted -= HandleGameRestarted;
    }

    private void Start()
    {
        UnsecuredEventBus.TriggerHealthChanged(_lives);
    }

    private void HandlePlayerDied()
    {
        _lives--;
        UnsecuredEventBus.TriggerHealthChanged(_lives); 

        if (_lives > 0)
        {
            Invoke(nameof(RestartLevel), _restartDelay);
        }
        else
        {
            UnsecuredEventBus.TriggerGameLose();
            Invoke(nameof(RestartGame), _restartDelay);
        }
    }

    private void HandleGameRestarted()
    {

    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UnsecuredEventBus.TriggerLevelRestarted();
    }

    private void RestartGame()
    {
        _lives = _maxLives;
        UnsecuredEventBus.TriggerHealthChanged(_lives);
        ScoreManager.Instance.ResetScore();
        SceneManager.LoadScene(_firstLevelScene);
        UnsecuredEventBus.TriggerLevelRestarted();
    }

    public int Lives => _lives;
    public int MaxLives => _maxLives;
}
